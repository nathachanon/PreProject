var AddRT = function () {
    var data = $("#rtForm").serialize();
    $.ajax({
        type: "post",
        url: "/Manage/AddRT",
        data: data,
        success: function (result) {
            if (result == "Success") {
                swal({
                    position: 'center',
                    type: 'success',
                    title: 'กำหนดประเภทห้องสำเร็จ',
                    showConfirmButton: false,
                    timer: 2000
                })
            } else {
                swal({
                    position: 'center',
                    type: 'error',
                    title: 'ระบบขัดข้อง' + result,
                    showConfirmButton: false
                })
            }
        }
    })
}
