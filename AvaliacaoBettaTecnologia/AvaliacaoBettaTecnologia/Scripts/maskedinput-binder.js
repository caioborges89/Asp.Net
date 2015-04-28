$(function () {
    $('[mask]').each(function (e) {
        $(this).mask($(this).attr('mask'));
    });
});

jQuery(function ($) {
    $("#data").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
    $("#telefone").mask("(999) 999-9999");
});