// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('body').on('click', '.btn-add-to-cart', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    const culture = $('#hidCulture').val();
    $.ajax({
        url: '/' + culture + '/Cart/AddToCart',
        data:
        {
            id: id,
            languageId: culture
        },
        type: 'POST',
        success: function (res) {
            if (res.status == true) {
                alert('Add to cart successful');
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
})