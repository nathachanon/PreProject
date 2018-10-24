$(document).on('click', '#close-preview', function () {
    $('.mpv').popover('hide');

    $('.mpv').hover(
        function () {
            $('.mpv').popover('show');
        },
        function () {
            $('.mpv').popover('hide');
        }
    );
});


$(function () {

    var closebtn = $('<button/>', {
        type: "button",
        text: 'x',
        id: 'close-preview',
        style: 'font-size: initial;',
    });
    closebtn.attr("class", "close pull-right");

    $('.mpv').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>ตัวอย่าง</strong>" + $(closebtn)[0].outerHTML,
        content: "ไม่มีรูปภาพ",
        placement: 'bottom'

    });


    $(".image-preview-input input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#icon").removeClass("mdi mdi-city mdi-24px text-white").addClass("mdi mdi-image-area-close mdi-24px text-white");
            $(".img-title").removeClass().addClass("btn btn-default  bg-info border-info image-preview-input  img-title")
            $(".image-preview-clear").show();
            $(".image-preview-filename").val(file.name);
            img.attr('src', e.target.result);
            $(".mpv").attr("data-content", $(img)[0].outerHTML).popover("show");

        }
        reader.readAsDataURL(file);
    });

    $(".file-preview-input input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();

        reader.onload = function (e) {
            $(".img-title").removeClass().addClass("btn btn-default  bg-info border-info file-preview-input  img-title")
            $(".file-preview-clear").show();
            $(".file-preview-filename").val(file.name);
            img.attr('src', e.target.result);
            $(".mpv").attr("data-content", $(img)[0].outerHTML).popover("show");

        }
        reader.readAsDataURL(file);
    });
});
