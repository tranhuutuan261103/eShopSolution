var CartController = function () {
    this.initialize = function () {
		loadData();
		registerEvents();
    }

    function loadData() {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: 'GET',
            url: '/' + culture + '/Cart/GetListItems',
            dataType: 'json',
			success: function (res) {
				console.log(res);
				if (res.length == 0) {
					$('#table_cart').hide();
					$('#lbl_number_of_items').text(0);
					$('#lbl_number_items_header').text(0);
					return;
				}
				var html = '';
				var total = 0;

				$.each(res, function (i, item) {
					html += "<tr>"
						+ "	    <td> <img width=\"60\" src=\"" + "https://localhost:7040/user-content/" + item.image + "\" alt=\"\" /></td>"
						+ "		<td>" + item.name + "<br />" + item.description + "</td>"
						+ "		<td>"
						+ "			<div class=\"input-append\">"
						+ "				<input class=\"span1\" style=\"max-width:34px\""
						+ "					placeholder=\"" + item.quantity + "\" id=\"txt_quantity_" + item.productId + "\" value=\"" + item.quantity + "\" size=\"16\" type=\"text\">"
						+ "				<button class=\"btn btn-minus\" data-id=\"" + item.productId +"\" type=\"button\">" // btn-minus
						+ "					<i class=\"icon-minus\"></i>"
						+ "				</button>"
						+ "				<button class=\"btn btn-plus\" data-id=\"" + item.productId +"\" type=\"button\"> " // btn-plus
						+ "					<i class=\"icon-plus\"></i> "
						+ "				</button> "
						+ "				<button class=\"btn btn-danger btn-remove\" data-id=\"" + item.productId +"\" type=\"button\">" //btn-remove
						+ "					<i class=\"icon-remove icon-white\"></i>"
						+ "				</button>"
						+ "			</div>"
						+ "		</td>"
						+ "		<td>" + item.price + "</td>"
						+ "		<td>" + item.price * item.quantity + "</td>"
						+ "	</tr>";
					total += item.price * item.quantity;
				});
				$('#cart_body').html(html);
				$('#lbl_number_of_items').text(res.length);
				$('#lbl_cart_total').text(total);
            }
        });
	}

	function registerEvents() {
		$('body').on('click', '.btn-plus', function (e) {
            e.preventDefault();
			const id = $(this).data('id');
			var quantity = $('#txt_quantity_' + id).val();
			var newQuantity = parseInt(quantity) + 1;
			updateCart(id, newQuantity);
		});

		$('body').on('click', '.btn-minus', function (e) {
			e.preventDefault();
			const id = $(this).data('id');
			var quantity = $('#txt_quantity_' + id).val();
			var newQuantity = parseInt(quantity) - 1;
			updateCart(id, newQuantity);
		});

		$('body').on('click', '.btn-remove', function (e) {
			e.preventDefault();
			const id = $(this).data('id');
			var newQuantity = 0;
			updateCart(id, newQuantity);
		});
	}

	function updateCart(id, quantity) {
		const culture = $('#hidCulture').val();
        $.ajax({
            type: 'POST',
            url: '/' + culture + '/Cart/UpdateCart',
            data:
            {
                id: id,
                quantity: quantity,
                languageId: culture
            },
            success: function (res) {
				loadData();
				$('#lbl_number_items_header').text(res.length);
				$('#lbl_number_of_items').text(res.length);
            },
            error: function (e) {
                console.log(e);
            }
        });
	}
}