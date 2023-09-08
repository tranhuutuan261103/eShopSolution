// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var SiteController = function () {
    this.initialize = function () {
        registerEvents();
        loadCart();
    }

    function loadCart() {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: 'GET',
            url: '/' + culture + '/Cart/GetListItems',
            dataType: 'json',
            success: function (res) {
                $('#lbl_number_items_header').text(res.length);
            }
        });
    }

    function registerEvents() {
        $('body').on('click', '.btn-add-to-cart', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const culture = $('#hidCulture').val();
            $.ajax({
                type: 'POST',
                url: '/' + culture + '/Cart/AddToCart',
                data:
                {
                    id: id,
                    languageId: culture
                },
                success: function (res) {
                    $('#lbl_number_items_header').text(res.length);
                    alert('Add to cart successfully');
                },
                error: function (e) {
                    console.log(e);
                }
            });
        })
    }
}

