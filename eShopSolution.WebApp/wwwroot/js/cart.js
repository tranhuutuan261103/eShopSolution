var CartController = function () {
    this.initialize = function () {
        loadData();
    }

    function loadData() {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: 'GET',
            url: '/' + culture + '/Cart/GetListItems',
            dataType: 'json',
			success: function (res) {
				console.log(res);
				var html = '';
				var total = 0;

				$.each(res, function (i, item) {
					html += "<tr>"
						+ "	    <td> <img width=\"60\" src=\"" + "https://localhost:7040/user-content/" + item.image + "\" alt=\"\" /></td>"
						+ "		<td>" + item.name + "<br />" + item.description + "</td>"
						+ "		<td>"
						+ "			<div class=\"input-append\">"
						+ "				<input class=\"span1\" style=\"max-width:34px\""
						+ "					placeholder=\"" + item.quantity + "\" id=\"appendedInputButtons\" size=\"16\" type=\"text\"><button class=\"btn\" type=\"button\">"
						+ "				<i class=\"icon-minus\"></i>"
						+ "				</button><button class=\"btn\" type=\"button\"> "
						+ "					<i class=\"icon-plus\"></i> "
						+ "				</button> "
						+ "				<button class=\"btn btn-danger\" type=\"button\">"
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
}