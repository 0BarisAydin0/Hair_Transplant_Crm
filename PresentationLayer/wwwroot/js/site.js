// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(document).on('click', '[data-toggle="lightbox"]', function (event) {
//    event.preventDefault();
//    $(this).ekkoLightbox({ alwaysShowClose: true });
//});


$(window).on("load", function () {
    // Loader elementini al
    var loader = $("#loader");
    // Content elementini al
    var content = $("#content");

    // İçeriği göster
    content.show();

    // Loader'ı kaldır (fadeOut kullanarak bir animasyon ekleyebilirsiniz)
    loader.fadeOut();
});