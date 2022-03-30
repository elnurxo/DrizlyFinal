$(function () {
	//BRAND SLIDER
	$('.brand-active-carousel').slick({
		arrows: false,
		slidesToShow: 4,
		autoplay: true,
		autoplaySpeed: 1000,
		responsive: [
			{
				breakpoint: 992,
				settings: {
					slidesToShow: 2
				}
			},
			{
				breakpoint: 480,
				settings: {
					slidesToShow: 1
				}
			}
		]
	});
	//ADD SLICK SLIDER IF COUNT GREATER THAN 6
	$(".category-slide").each(function (index) {
			$(this).find(".drink-slider").slick({
				arrows: true,
				infinite: true,
				slidesToShow: 6,
				slidesToScroll: 1,
				autoplay: false,
				autoplaySpeed: 1200,
				responsive: [
					{
						breakpoint: 1400,
						settings: {
							arrows: false
						}
					},

					{
						breakpoint: 992,
						settings: {
							arrows: false,
							slidesToShow: 4
						}
					},
					{
						breakpoint: 768,
						settings: {
							arrows: false,
							slidesToShow: 2
						}
					},
					{
						breakpoint: 480,
						settings: {
							arrows: false,
							slidesToShow: 1
						}
					}
				]
			});
	});

	//PRODUCTS-SHOP-SLIDER
	//$('.drink-slider').slick({
	//	arrows: true,
	//	infinite: true,
	//	slidesToShow: 6,
	//	slidesToScroll: 1,
	//	autoplay: true,
	//	autoplaySpeed: 1200,
	//	responsive: [
	//		{
	//			breakpoint: 1400,
	//			settings: {
	//				arrows: false
	//			}
	//		},

	//		{
	//			breakpoint: 992,
	//			settings: {
	//				arrows: false,
	//				slidesToShow: 4
	//			}
	//		},
	//		{
	//			breakpoint: 768,
	//			settings: {
	//				arrows: false,
	//				slidesToShow: 2
	//			}
	//		},
	//		{
	//			breakpoint: 480,
	//			settings: {
	//				arrows: false,
	//				slidesToShow: 1
	//			}
	//		}
	//	]
	//});
});
