function exportFiles() {
    document.getElementById("aVehicleBrandsDownload").click();
    downloadFile('/VehicleBrand/ExportFamilies', 'Families.xlsx');

}

function importFile() {
    var fileName = document.getElementById('fileImport').files[0].name;
    swal({
        title: "Are you sure?",
        text: "Do you want import file ( " + fileName + " )",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    }, function (isConfirm) {
        if (isConfirm) {
            var files = $("#fileImport").get(0).files;

            var formData = new FormData();
            formData.append('file', files[0]);

            $.ajax({
                url: '/VehicleBrand/ImportFile',
                data: formData,
                type: 'POST',
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.status == 1) {
                        swal("Successful", "Added successfully.", "success");
                        document.getElementById("linkAllVehicleBrands").click();
                    } else if (response.status == 0) {
                        swal("Cancelled", response.message, "error");
                    }
                    document.getElementById("fileImport").value = "";
                }
            })
        } else {
            document.getElementById("fileImport").value = "";
        }
    })
}