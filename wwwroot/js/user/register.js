function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function phonenumber(inputtxt) {
    var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    return phoneno.test(inputtxt);
}

var Register1 = function () {
    var password = $("#Password").val();
    var cfmpassword = $("#cfmPassword").val();
    if (cfmpassword != password) {
        swal({
            type: "error",
            title: 'เกิดข้อผิดพลาด...',
            text: 'รหัสผ่าน ไม่ตรงกัน !',
        });
    } else {
        Register();
    }
}

function gotologin() {
    setTimeout(function () {
        window.location.href = "../User/Login";
    }, 2000);
}
var Register = function () {
    var data = $("#registerForm").serialize();
    var email = $("#email").val();
    var name = $("#name").val();
    var surname = $("#surname").val();
    var tel = $("#tel").val();
    var password = $("#Password").val();

    if (email == "" | name == "" | surname == "" | tel == "" | password == "") {
        swal({
            position: 'center',
            type: 'error',
            title: 'เกิดข้อผิดพลาด',
            text: 'กรุณากรอกข้อมูลให้ครบถ้วน ก่อนสมัคสมาชิก !',
            showConfirmButton: false,
            timer: 3000
        });
    } else {
        if (validateEmail(email)) {

            if (phonenumber(tel)) {
                $.ajax({
                    type: "post",
                    url: "/User/Register",
                    data: data,
                    success: function (result) {
                        if (result == "Fail") {
                            $('#email').val("");
                            swal({
                                position: 'center',
                                type: 'error',
                                title: 'Email นี้ถูกใช้งานไปแล้ว',
                                showConfirmButton: false,
                                timer: 3000
                            });
                        } else {
                            swal({
                                position: 'center',
                                type: 'success',
                                title: 'ลงทะเบียนสำเร็จ',
                                showConfirmButton: false,
                                timer: 2000
                            });
                            gotologin();
                        }
                    }
                });
            } else {
                $('#tel').val("");
                swal({
                    position: 'center',
                    type: 'error',
                    title: 'รูปแบบของเบอร์โทรศัพท์ไม่ถูกต้อง',
                    showConfirmButton: false,
                    timer: 3000
                });
            }
        } else {
            swal({
                position: 'center',
                type: 'error',
                title: 'รูปแบบ Email ผิดพลาด',
                showConfirmButton: false,
                timer: 3000
            });
        }
    }
}