// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
	var count = 1;

	$('.js-show-more').on('click', function (e) {
		e.preventDefault();

		if (count < 3) {
			$('.form__row.active').next().slideDown().addClass('active');
			count++;
		} else {
			$('.js-show-note').slideDown();
        }
	});
});