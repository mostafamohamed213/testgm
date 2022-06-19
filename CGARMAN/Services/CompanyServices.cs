using CGARMAN.ViewModel;
using RepositoryPatternWithUOW.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.Consts;
using CGARMAN.ViewModel.TechnicianViewModels;
using CGARMAN.ViewModel.Shared;
using Microsoft.AspNetCore.Http;
using System.IO;
using ExcelDataReader;

namespace CGARMAN.Services
{   
    public class CompanyServices
    {
        IUnitOfWork unitOfWork;
        public CompanyServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork; 
        }

        public PagingViewModel<TechnicianCompany> getAllCompaniesPaging(int currentPage)
        {            
            PagingViewModel<TechnicianCompany> model = new PagingViewModel<TechnicianCompany>();
            var Companies = unitOfWork.TechnicianCompany.FindAll(c=> c.Enable, (currentPage - 1) * TablesMaxRows.CompaniesIndex, TablesMaxRows.CompaniesIndex, d => d.TechnicianCompanyId, OrderBy.Ascending);
            model.items = Companies.ToList();
            var itemsCount = unitOfWork.TechnicianCompany.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.CompaniesIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.CompaniesIndex;
            return model;
        }
        public PagingViewModel<TechnicianCompany> getAllCompaniesPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.TechnicianIndex = length;
            return getAllCompaniesPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.TechnicianCompany.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.TechnicianCompanyId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.TechnicianCompany.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.TechnicianCompanyId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(string name)
        {
            TechnicianCompany company = new TechnicianCompany()
            {
                Name = name.Trim(),
                CreateDts = DateTime.Now,
                Enable = true,
            };
            unitOfWork.TechnicianCompany.Add(company);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name,int id=0)
        {
           return unitOfWork.TechnicianCompany.GetOne(c => c.Enable && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.TechnicianCompanyId != id) == null ? true : false; 
        }

        internal TechnicianCompany getCompanyById(int id)
        {
            return unitOfWork.TechnicianCompany.GetOne(c => c.Enable && c.TechnicianCompanyId == id );
        }

        internal PagingViewModel<TechnicianCompany> Search(string Name,int currentPage)
        {
            var Companies = unitOfWork.TechnicianCompany.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false,null);
            PagingViewModel<TechnicianCompany> model = new PagingViewModel<TechnicianCompany>();
            model.items = Companies.ToList();
            var itemsCount = Companies.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(EditCompanyViewModel model)
        {
           var company= getCompanyById(model.TechnicianCompanyId);
            company.Name = model.Name.Trim();           
            unitOfWork.Complete();                
        }

        internal void Delete(int technicianCompanyId)
        {
            var company = getCompanyById(technicianCompanyId);           
            company.Enable =false;
            unitOfWork.Complete();
        }


        internal ImportFileStatus GetDataFromCSVFile(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reder = ExcelReaderFactory.CreateReader(stream))
            {
                try
                {
                    if (reder.RowCount <= 1)
                    {
                        return new ImportFileStatus { status = 0, message = $"This file is empty." };
                    }

                    List<TechnicianCompany> companies = new List<TechnicianCompany>();

                    List<string> existCompanies = unitOfWork.TechnicianCompany.GetAll().Select(c=>c.Name.ToLower()).ToList();
                    List<string> existList = new List<string>();

                    DateTime now = DateTime.Now;              

                    int rowNumber = 1;
                    while (reder.Read())
                    {
                        if (rowNumber > 1)// ignore header
                        {
                            existList = companies.Select(c => c.Name.ToLower()).ToList();
                            //validation
                            if (reder.GetValue(0) == null || string.IsNullOrWhiteSpace(reder.GetValue(0).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name." };
                            if (existCompanies.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (This is company already exist)." };
                            if (existList.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (duplicate values)" };

                            companies.Add(new TechnicianCompany
                            {
                                Name = reder.GetValue(0).ToString().Trim(),
                                CreateDts = now,
                                Enable = true,                          
                            });
                        }
                        rowNumber++;
                    }
               
                    unitOfWork.TechnicianCompany.AddRange(companies);
                    unitOfWork.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is IndexOutOfRangeException)
                    {
                        return new ImportFileStatus { status = 0, message = $"File format is not valid" };
                    }
                    return new ImportFileStatus { status = 0, message = $"Error : {ex.Message}" };
                }
            }
            return new ImportFileStatus { status = 1, message = "Successful" };
        }
    }
}
