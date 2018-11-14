$(document).ready(function () {
    $('#water').popover({ title: 'มิเตอร์น้ำเดือนที่แล้ว', content: "กรุณาเลือกห้องก่อน" });
    $('#elec').popover({ title: 'มิเตอร์ไฟเดือนที่แล้ว', content: "กรุณาเลือกห้องก่อน" });
    var d = new Date();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    document.getElementById("month").value = year + "-" + month;
});
$("#goback").click(function () {
    $("#roomid").val('');
    $("#water").val('');
    $("#elec").val('');
    document.getElementById("divadds").innerHTML = '';
    document.getElementById("showmonth").innerHTML = '';
    $("#div1").hide("fast");
    $("#months").fadeIn("fast");
});
$("#next1").click(function () {
    var d = new Date();
    var getmonth = d.getMonth() + 1
    var datemonth_now = d.getFullYear() + "-" + getmonth;
    var datemonth = $("#month").val();
    if (datemonth <= datemonth_now) {
        document.getElementById("month").stepDown(1);
        var datemonths = $("#month").val();
        document.getElementById("month").value = datemonth;
        if (datemonth != "") {
            $.ajax({
                type: "post",
                url: "../Renter/getRenterMeter2",
                success: function (result) {
                    if (result == "Fail") {
                        swal({
                            position: 'center',
                            type: 'error',
                            title: 'เกิดข้อผิดพลาด',
                            showConfirmButton: false,
                            timer: 3000
                        });
                    } else {
                        var items = '';
                        $.each(result, function (i, item) {
                            $("#divadds").append("<button onclick='getroom(" + item.roomid + ")' class = 'mr-1 btn-default btn btn-1 bg-info border-info text-white'>" + item.roomid + "</button>");
                        });
                        $("#months").hide("fast");
                        $("#showmonth").text("กรอกมิเตอร์ของเดือน " + datemonth);
                        $("#div1").fadeIn("fast");
                    }
                }
            })
        } else {
            alert("This is Month : NULL Pls input Month !");
        }
    } else {
        alert("กรุณาเลือกเดือนและปีให้ตรงกับปัจจุบัน หรือ น้อยกว่า ไม่สามารถเลือกที่มากกว่าปัจจุบันได้")
    }
});

$("#add").click(function () {
    var datemonth = $("#month").val();
    var roomid = $("#roomid").val();
    var water = $("#water").val();
    var elec = $("#elec").val();
    var oldwater = $('#water').attr('data-content');
    var oldelec = $('#elec').attr('data-content');
    if (roomid == 0 | water == 0 | elec == 0) {
        swal({
            position: 'center',
            type: 'error',
            title: 'เกิดข้อผิดพลาด',
            text: "กรุณากรอกข้อมูลให้ถูกต้องครบถ้วน และ เลือกห้องให้เรียบร้อย",
            showConfirmButton: false,
            timer: 3000
        });
    } else {
        if (oldwater >= water | oldelec >= elec) {
            swal({
                position: 'center',
                type: 'error',
                title: 'เกิดข้อผิดพลาด',
                text: "มิเตอร์น้ำ และ มิเตอร์ไฟ ต้องมีค่ามากกว่าเดือนที่แล้ว !",
                showConfirmButton: false,
                timer: 3000
            });
        } else {
            $.ajax({
                type: "post",

                url: "../Renter/AddCal?datemonth=" + datemonth + "&roomid=" + roomid + "&meterwater=" + water + "&meterelec=" + elec + "&oldwater=" + oldwater + "&oldelec=" + oldelec,
                success: function (result) {
                    if (result == "Fail") {
                        swal({
                            position: 'center',
                            type: 'error',
                            title: 'เกิดข้อผิดพลาด',
                            text: "ระบบขัดข้อง",
                            showConfirmButton: false,
                            timer: 3000
                        });
                    } else if (result == "RoomTypeError") {
                        swal({
                            position: 'center',
                            type: 'error',
                            title: 'เกิดข้อผิดพลาด',
                            text: "ไม่สามารถทำรายการได้ เนื่องจาก ยังไม่เลือกประเภทของห้อง",
                            showConfirmButton: false,
                            timer: 3000
                        });
                    }
                    else {
                        swal({
                            position: 'center',
                            type: 'success',
                            title: 'กรอกข้อมูลสำเร็จ',
                            text: "ระบบได้ทำการคำนวณค่าน้ำค่าไฟให้เรียบร้อยแล้ว !",
                            showConfirmButton: false,
                            timer: 3000
                        });
                        $("#roomid").val('');
                        $("#water").val('');
                        $("#elec").val('');
                    }
                }
            })
        }
    }
});

function getroom(a) {
    $("#roomid").val(a);
    var datemonth = $("#month").val();
    document.getElementById("month").stepDown(1);
    var datemonths = $("#month").val();
    document.getElementById("month").value = datemonth;
    var roomids = a;
    $.ajax({
        type: "post",
        url: "../Renter/RenterCheckMonth?roomid=" + roomids,
        success: function (result) {
            if (result != "") {
                if (result > datemonth) {
                    swal({
                        position: 'center',
                        type: 'error',
                        title: 'เกิดข้อผิดพลาด',
                        text: "ห้องที่เลือกวันที่เริ่มเช่ามากกว่าเดือนที่ต้องการเพิ่มมิเตอร์",
                        showConfirmButton: false,
                        timer: 3000
                    });
                    $("#roomid").val('');
                    $('#water').attr('data-content', "กรุณาเลือกห้องก่อน");
                    $('#elec').attr('data-content', "กรุณาเลือกห้องก่อน");
                } else {
                    $.ajax({
                        type: "post",
                        url: "../Renter/getRenterMeter?datemonth=" + datemonth + "&roomid=" + roomids + "&datemonth_old=" + datemonths,
                        success: function (result) {
                            if (result.result == "Fail") {
                                swal({
                                    position: 'center',
                                    type: 'error',
                                    title: 'เกิดข้อผิดพลาด',
                                    text: "ห้องนี้ได้เพิ่มค่าน้ำค่าไฟของเดือนนี้ไปแล้ว",
                                    showConfirmButton: false,
                                    timer: 3000
                                });
                                $("#roomid").val('');
                                $('#water').attr('data-content', "กรุณาเลือกห้องก่อน");
                                $('#elec').attr('data-content', "กรุณาเลือกห้องก่อน");
                            } else {
                                $('#water').attr('data-content', result.water);
                                $('#elec').attr('data-content', result.elec);
                            }
                        }
                    });
                }
            } else {
                $.ajax({
                    type: "post",
                    url: "../Renter/getRenterMeter?datemonth=" + datemonth + "&roomid=" + roomids + "&datemonth_old=" + datemonths,
                    success: function (result) {
                        if (result.result == "Fail") {
                            swal({
                                position: 'center',
                                type: 'error',
                                title: 'เกิดข้อผิดพลาด',
                                text: "ห้องนี้ได้เพิ่มค่าน้ำค่าไฟของเดือนนี้ไปแล้ว",
                                showConfirmButton: false,
                                timer: 3000
                            });
                            $("#roomid").val('');
                            $('#water').attr('data-content', "กรุณาเลือกห้องก่อน");
                            $('#elec').attr('data-content', "กรุณาเลือกห้องก่อน");
                        } else {
                            $('#water').attr('data-content', result.water);
                            $('#elec').attr('data-content', result.elec);
                        }
                    }
                });
            }
        }
    });
};