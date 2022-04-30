using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CGARMAN.ViewModel.Vehicle
{
    public class LinkTrailerViewModel
    {
        public long VehicleID { get; set; }
        public RepositoryPatternWithUOW.Core.Models.Vehicle Vehicle { get; set; }
        public SelectList Families { get; set; }
     
        [Remote("ValidatePlateNumber", "Vehicle", ErrorMessage = "here is no trailer with this plate number or this trailer linked by another vehicle ")]
       //  [Remote("ValidateplateNumberIslinkedByAnotherVehicle", "Vehicle", ErrorMessage = "this trailer linked by another vehicle ", AdditionalFields = "VehicleID")]
       // [Remote("ValidateplateNumberIslinkedByAnotherVehicle", "Vehicle", ErrorMessage = "There is no vehicle with this plate number", AdditionalFields = "VehicleID")]

        public string PlateNumberTrailer { get; set; }
        [Remote("ValidatePlateNumber2", "Vehicle", ErrorMessage = "this trailer linked by another vehicle ")]     
        public long TrailerID { get; set; }
    }
}
