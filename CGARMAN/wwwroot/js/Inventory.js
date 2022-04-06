function getClassificationsManagementPage() {
    ToggleColorInDashboard("ClassificationsManagement");
    getspinner();
    changeTitle("Classifications Management");

    $.ajax({
        type: "Post",
        url: "/InventoryClassifications/Index",
        data: { "CurrentPageIndex": 1 },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function getBrandManagementPage() {
    ToggleColorInDashboard("BrandManagement");
    getspinner();
    changeTitle("Brand Management");

    $.ajax({
        type: "Post",
        url: "/InventoryBrand/Index",
        data: { "CurrentPageIndex": 1 },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function getModelManagementPage() {
    ToggleColorInDashboard("ModelManagement");
    getspinner();
    changeTitle("Model Management");

    $.ajax({
        type: "Post",
        url: "/InventoryModel/Index",
        data: { "CurrentPageIndex": 1 },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function getUnitsManagementPage() {
    ToggleColorInDashboard("UnitManagement");
    getspinner();
    changeTitle("Unit Management");

    $.ajax({
        type: "Post",
        url: "/InventoryUnit/Index",
        data: { "CurrentPageIndex": 1 },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function getVendorManagementPage() {
    ToggleColorInDashboard("VendorManagement");
    getspinner();
    changeTitle("Suppliers Management");

    $.ajax({
        type: "Post",
        url: "/InventoryVendor/Index",
        data: { "CurrentPageIndex": 1 },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function getStatusManagementPage() {
    ToggleColorInDashboard("StatusManagement");
    getspinner();
    changeTitle("Status Management");

    $.ajax({
        type: "Post",
        url: "/InventoryStatus/Index",
        data: { "CurrentPageIndex": 1 },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function gotoItemType(itemtypeId) {

    getspinner();
    var item = parseInt(itemtypeId);
    $.ajax({
        type: "Get",
        url: "/InventoryItemType/ViewItemType",
        data: { "id": item },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
            openTablink(event, 'divOnHand');
            var element = document.getElementById("btnOnHand");
            element.classList.add("active");
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function submitInvItemType() {
    ToggleColorInDashboard("ItemNameScreen");

    changeTitle("Item");
    var form = document.getElementById('frmCreateInvItemType');
    var formData = new FormData(form);
    getspinner();
    $.ajax({
        type: "Post",
        url: "/InventoryItemType/Create",
        data: formData,
        processData: false,
        ///* processData: false,*/
        contentType: false,
        success: function (response) {

            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
            $("#btnItemv").click();
            $("#btnGeneralInformation").click();
            var val = $("#WarehouseIdityp").val();
            GetClassificationsCostCenterAndUnitsss(val);

        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });

}


function submitEditInvItemType() {
    ToggleColorInDashboard("ItemNameScreen");

    changeTitle("Item");
    var form = document.getElementById('frmEditInvItemType');
    var formData = new FormData(form);
    getspinner();
    $.ajax({
        type: "Post",
        url: "/InventoryItemType/Edit",
        data: formData,
        processData: false,
        ///* processData: false,*/
        contentType: false,
        success: function (response) {

            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
            //$("#btnItemv").click();
            //$("#btnGeneralInformation").click();
            //var val = $("#WarehouseIdityp").val();
            //GetClassificationsCostCenterAndUnitsss(val);

        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });

}



function PagerClick(index) {
    document.getElementById("hfCurrentPageIndex").value = index;
    $.ajax({
        type: "Post",
        url: "/InventoryItem/IndexPage",
        data: { "CurrentPageIndex": index },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });

}
function changeTablelength(e) {
    ToggleColorInDashboard("ItemsScreen");
    getspinner();
    changeTitle("Index");
    $.ajax({
        type: "Post",
        url: "/InventoryItem/Changelength",
        data: { "CurrentPageIndex": 1, "length": e.value },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function PagerClickfortype(index) {

    document.getElementById("hfCurrentPageIndex").value = index;
    getspinner();
    $.ajax({
        type: "Post",
        url: "/InventoryItemType/IndexPage",
        data: { "CurrentPageIndex": index },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });

}
function changeTablelengthfortype(e) {
    ToggleColorInDashboard("ItemNameScreen");
    getspinner();
    changeTitle("Index");
    $.ajax({
        type: "Post",
        url: "/InventoryItemType/Changelength",
        data: { "CurrentPageIndex": 1, "length": e.value },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function changeTablelengthfortypestatus(e) {

    ToggleColorInDashboard("StatusManagement");
    getspinner();
    changeTitle("Status Management");
    $.ajax({
        type: "Post",
        url: "/InventoryStatus/Changelength",
        data: { "CurrentPageIndex": 1, "length": e.value },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function changeTablelengthforInventoryClassifications(e) {

    ToggleColorInDashboard("ClassificationsManagement");
    getspinner();
    changeTitle("Classification Management");
    $.ajax({
        type: "Post",
        url: "/InventoryClassifications/Changelength",
        data: { "CurrentPageIndex": 1, "length": e.value },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function changeTablelengthforInventoryBrand(e) {

    ToggleColorInDashboard("BrandManagement");
    getspinner();
    changeTitle("Brand Management");
    $.ajax({
        type: "Post",
        url: "/InventoryBrand/Changelength",
        data: { "CurrentPageIndex": 1, "length": e.value },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function changeTablelengthforInventoryModel(e) {

    ToggleColorInDashboard("ModelManagement");
    getspinner();
    changeTitle("Model Management");
    $.ajax({
        type: "Post",
        url: "/InventoryModel/Changelength",
        data: { "CurrentPageIndex": 1, "length": e.value },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function changeTablelengthforVendor(e) {

    ToggleColorInDashboard("VendorManagement");
    getspinner();
    changeTitle("Vendor Management");
    $.ajax({
        type: "Post",
        url: "/InventoryVendor/Changelength",
        data: { "CurrentPageIndex": 1, "length": e.value },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function saveInventoryItemType() {

    document.getElementById("divbgchangeItemType").style.backgroundColor = "#e6e6e6";
    document.getElementById("btnCteInventoryItemType1").disabled = true;
    document.getElementById("alertErrorItemType").style.display = "none";

    var form = document.getElementById('ss');
    var formData = new FormData(form);
    $.ajax({
        type: "Post",
        url: "/InventoryItemType/Create",
        data: formData,
        processData: false,
        ///* processData: false,*/
        contentType: false,
        success: function (response) {
            if (response == 1) {
                document.getElementById("divbgchangeItemType").style.backgroundColor = "#fff";
                document.getElementById("alertErrorItemType").style.display = "none";
                document.getElementById("btnCteInventoryItemType1").disabled = true;
                $("#btnCloseformCteInventoryItemType").click();
                $("#successModal").click();
            } else {


                document.getElementById("alertErrorItemType").style.display = "block";
                document.getElementById("divbgchangeItemType").style.backgroundColor = "#fff";
                document.getElementById("btnCteInventoryItemType1").disabled = false;
            }

        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });

}

function invItemNamess() {
    ToggleColorInDashboard("ItemNameScreen");
    getspinner();
    changeTitle("Item Name");

    $.ajax({
        type: "Post",
        url: "/InventoryItemType/IndexPage",
        data: { "CurrentPageIndex": 1 },
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function createInvItemName() {
    ToggleColorInDashboard("CreateItemNameScreen");
    getspinner();
    changeTitle("Create Item");
    $.ajax({
        type: "Get",
        url: "/InventoryItemType/Create",
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);

        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function createInvItemNameforview(f) {
    var formData = new FormData(f);
    ToggleColorInDashboard("none");
    getspinner();
    changeTitle("Home");


    $.ajax({
        type: "Post",
        url: "/InventoryItemType/Create",
        data: formData,
        processData: false,
        ///* processData: false,*/
        contentType: false,
        success: function (response) {
            if (response == 1) {
                $("#ChangingBody").empty();
                $('#ChangingBody').append(response);
            } else {

                $("#ChangingBody").empty();
                $('#ChangingBody').append(response);
            }

        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function createInvItem() {
    ToggleColorInDashboard("CreateItemScreen");
    getspinner();
    changeTitle("Create Item");
    $.ajax({
        type: "Get",
        url: "/InventoryItem/Create",
        success: function (response) {
            $("#ChangingBody").empty();
            $('#ChangingBody').append(response);
            onChangeselCodeType(-1);
            getModelByBrandID(-1);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function GetSubWarehouseStructure(WarehouseId) {
    $.ajax({
        type: "Get",
        url: "/InventoryWarehouse/GetSubWarehouseStructure",
        data: { "warehouseId": WarehouseId },
        success: function (response) {

            if (response == 'null') {
                document.getElementById("dvWarehousesstructure").style.display = "none";
            } else {
                document.getElementById("dvWarehousesstructure").style.display = "block";
                $("#dvSelSubWarehouse").empty();
                $('#dvSelSubWarehouse').append(response);


            }
            /*  GetinvTypes(WarehouseId);*/
            //$("#SelStatusWarehouse").empty();
            //$('#SelStatusWarehouse').append('<select id="SelStatusWarehouse123" class="form-control form-control-user"><option> Please select one</option></select>');
            //$('#inptSerialNumber').val("");
            //$("#inptSerialNumber").prop("disabled", false);
            onChangeselCodeType(-1);
            /* getModelByBrandID(-1);*/
            getFieldsbyWarehouseId(WarehouseId);
            /* $("#selCodeType").empty();*/
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function getFieldsbyWarehouseId(warehouseId) {

    if (warehouseId == 1) {
        /* $('#inputPartNumber').val("");*/
        document.getElementById("divPartNumber").style.display = "flex";
    } else {
        $('#inputPartNumber').val("");
        document.getElementById("divPartNumber").style.display = "none";
    }
    if (warehouseId == 3) {
        /* $('#selViscosity').val("");*/
        document.getElementById("divViscosity").style.display = "flex";
        getValuesViscosity();
    } else {
        $('#selViscosity').append("<option>Empty</option>");
        document.getElementById("divViscosity").style.display = "none";
    }

    if (warehouseId == 2) {

        document.getElementById("TireFields").style.display = "block";
        document.getElementById("divThresholdss").style.display = "flex";
        /*  document.getElementById("divStandardTreadDepth").style.display = "flex";*/
        getValuesTireSizeAndTirePattern();
    } else {
        $('#selTireSize').append("<option>Empty</option>");
        $('#selTirePattern').append("<option>Empty</option>");
        $('#selStandardTreadDepth').empty();
        $('#inputThreshold').empty();
        document.getElementById("TireFields").style.display = "none";
        document.getElementById("divThresholdss").style.display = "none";

        /*  document.getElementById("divStandardTreadDepth").style.display = "none";*/
    }
}

function getValuesTireSizeAndTirePattern() {
    $.ajax({
        type: "Get",
        url: "/InventoryWarehouse/getValuesTireSizeAndTirePattern",
        /* data: { "warehouseId": WarehouseId },*/
        success: function (response) {

            $('#selTireSize').empty();
            if (response.tireSizes != 'null') {
                response.tireSizes.forEach(function (i) {
                    var option = document.createElement("option");
                    option.text = i.text;
                    option.value = i.value;
                    $('#selTireSize').append(option);
                });
            } else {
                var option = document.createElement("option");
                option.text = "No Result";
                $('#selTireSize').append(option);
            }
            $('#selTirePattern').empty();
            if (response.tirePatterns != 'null') {
                response.tirePatterns.forEach(function (i) {
                    var option = document.createElement("option");
                    option.text = i.text;
                    option.value = i.value;
                    $('#selTirePattern').append(option);
                });
            } else {
                var option = document.createElement("option");
                option.text = "No Result";
                $('#selTirePattern').append(option);
            }


        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function getValuesViscosity() {
    $.ajax({
        type: "Get",
        url: "/InventoryWarehouse/getValuesViscosity",
        /* data: { "warehouseId": WarehouseId },*/
        success: function (response) {
            $('#selViscosity').empty();
            if (response != 'null') {
                response.forEach(function (i) {
                    var option = document.createElement("option");
                    option.text = i.text;
                    option.value = i.value;
                    $('#selViscosity').append(option);
                });
            } else {
                var option = document.createElement("option");
                option.text = "No Result";
                $('#selViscosity').append(option);
            }
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function GetinvTypes(WarehouseId) {
    $.ajax({
        type: "Get",
        url: "/InventoryWarehouse/GetWarehouseTypes",
        data: { "warehouseId": WarehouseId },
        success: function (response) {

            $("#dvSelItemsNameWarehouse").empty();
            $('#dvSelItemsNameWarehouse').append(response);
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function GetLaneWarehouseStructure(subWarehouseId) {
    $.ajax({
        type: "Get",
        url: "/InventoryWarehouse/GetLaneWarehouseStructure",
        data: { "subWarehouseId": subWarehouseId },
        success: function (response) {
            if (response == "null") {
                $("#dvSelLaneWarehouse").empty();
                $('#dvSelLaneWarehouse').append('<select class="form-control form-control-user"><option> Please select one</option></select>');
            } else {

                $("#dvSelLaneWarehouse").empty();
                $('#dvSelLaneWarehouse').append(response);
                $("#dvSelShelfWarehouse").empty();
                $('#dvSelShelfWarehouse').append('<select class="form-control form-control-user"><option> Please select one</option></select>');
            }
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function GetShelfWarehouseStructure(laneWarehouseId) {
    $.ajax({
        type: "Get",
        url: "/InventoryWarehouse/GetShelfWarehouseStructure",
        data: { "laneWarehouseId": laneWarehouseId },
        success: function (response) {
            if (response == "null") {
                $("#dvSelShelfWarehouse").empty();
                $('#dvSelShelfWarehouse').append('<select class="form-control form-control-user"><option> Please select one</option></select>');
            } else {

                $("#dvSelShelfWarehouse").empty();
                $('#dvSelShelfWarehouse').append(response);
            }
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function GetItemTypeStatusBrandCodetypeVendorIsQuantityAndIsAutoserial(itemType) {
    $.ajax({
        type: "Get",
        url: "/InventoryItemType/GetItemTypeStatusBrandCodetypeVendorIsQuantityAndIsAutoserial",
        data: { "itemType": itemType },

        success: function (response) {
            if (response.status != 'null') {
                $('#SelStatusWarehouse123').empty();
                response.status.forEach(function (i) {
                    var option = document.createElement("option");
                    option.text = i.text;
                    option.value = i.value;
                    $('#SelStatusWarehouse123').append(option);
                });
            }
            if (response.isAutoGenerateSerial) {
                $('#inptSerialNumber').val("Auto");
                $("#inptSerialNumber").prop("disabled", true);
            } else {
                $('#inptSerialNumber').val("");
                $("#inptSerialNumber").prop("disabled", false);
            }
            //if (response.codetypes != null) {
            //    $('#selCodeType').empty();
            //    var option = document.createElement("option");
            //    option.text = "No Code";
            //    option.value = -1;
            //    $('#selCodeType').append(option);
            //    response.codetypes.forEach(function (i) {
            //        var option = document.createElement("option");
            //        option.text = i.text;
            //        option.value = i.value;
            //        $('#selCodeType').append(option);
            //    });
            //     onChangeselCodeType(-1);
            //}
            //if (response.vendors != null) {
            //    $('#selSupplier').empty();

            //    response.vendors.forEach(function (i) {
            //        var option = document.createElement("option");
            //        option.text = i.text;
            //        option.value = i.value;
            //        $('#selSupplier').append(option);
            //    });
            //}
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function onChangeselCodeType(v) {
    if (v == -1) {
        $("#selCode").val("");
        $("#selCode").prop("disabled", true);
    } else {
        $("#selCode").val("");
        $("#selCode").prop("disabled", false);
    }
}

function getModelByBrandID(v) {
    if (v != null && v != "" && v > -1) {
        $.ajax({
            type: "Get",
            url: "/InventoryItem/getModelByBrandID",
            data: { "brandId": v },
            success: function (response) {
                if (response != 'null') {
                    $('#selModel').empty();
                    var option = document.createElement("option");
                    option.text = "Please select one";
                    /*  option.value = i.value;*/
                    $('#selModel').append(option);
                    response.forEach(function (i) {
                        var option = document.createElement("option");
                        option.text = i.text;
                        option.value = i.value;
                        $('#selModel').append(option);
                    });
                }
            },
            failure: function (response) {
                throwException(response)
            },
            error: function (response) {
                throwException(response)
            }
        });
    } else {
        $('#selModel').empty();
        var option = document.createElement("option");
        option.text = "Please select one";
        /*  option.value = i.value;*/
        $('#selModel').append(option);
    }

}
function getViewItemType(id) {

    $.ajax({
        type: "Get",
        url: "/InventoryItemType/ViewItemType",
        data: { "id": id },
        success: function (response) {
            if (response != 'null') {

                $("#ChangingBody").empty();
                $('#ChangingBody').append(response);
                //$("#btnItemv").click();
                //$("#btnGeneralInformation").click();
                openTablink(event, 'divShwItem');
                openTablinkInItem(event, 'divGeneralInformation');
                //var element = document.getElementById("btnItemv");
                ////element.style.backgroundColor = "#ccc";
                //element.classList.add("active");
                //var element2 = document.getElementById("btnGeneralInformation");
                ////element2.style.backgroundColor = "#ccc";
                //element2.classList.add("active");
                changecColorbtns('divShadowItemv');
            } else {
                alert("This item has been disabled");
            }
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function editInvItem(itemId) {

    $.ajax({
        type: "Get",
        url: "/InventoryItem/Edit",
        data: { "itemId": itemId },
        success: function (response) {

            if (response != 'null') {
                $("#ChangingBody").empty();
                $('#ChangingBody').append(response);


            } else {
                alert("Error");
            }
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function editInvItem(itemId) {

    $.ajax({
        type: "Get",
        url: "/InventoryItem/Edit",
        data: { "itemId": itemId },
        success: function (response) {

            if (response != 'null') {
                $("#ChangingBody").empty();
                $('#ChangingBody').append(response);


            } else {
                alert("Error");
            }
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function editDisplay(itemId) {

    $.ajax({
        type: "Get",
        url: "/InventoryItem/Display",
        data: { "itemId": itemId },
        success: function (response) {

            if (response != 'null') {
                $("#ChangingBody").empty();
                $('#ChangingBody').append(response);


            } else {
                alert("Error");
            }
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}
function getEditItemType(itemTypeId) {

    $.ajax({
        type: "Get",
        url: "/InventoryItemType/Edit",
        data: { "itemTypeId": itemTypeId },
        success: function (response) {

            if (response != 'null') {
                $("#ChangingBody").empty();
                $('#ChangingBody').append(response);


            } else {
                alert("Error");
            }
        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}

function editInventoryItem() {
    var form = document.getElementById('frmEditInvItem');
    var formData = new FormData(form);
    /* getspinner();*/
    $.ajax({
        type: "Post",
        url: "/InventoryItem/Edit",
        data: formData,
        processData: false,
        ///* processData: false,*/
        contentType: false,
        success: function (response) {
            if (response > 0) {
                swal("Don!", "success!", "success");
                getViewItemType(response);
            } else {
                swal("Cancelled", "Oops Something went wrong!", "error");

            }

        },
        failure: function (response) {
            throwException(response)
        },
        error: function (response) {
            throwException(response)
        }
    });
}