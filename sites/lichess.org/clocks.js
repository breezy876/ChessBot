(function(){
			var result = ['',''];

                           var div = $('.rclock-white');

                            if (div != null && div.length > 0) {
                                var child = div.children('.time');
                                result[0] = child[0].innerText;
                            }

                            div = $('.rclock-black');

                            if (div != null && div.length > 0) {
                                var child = div.children('.time');
                                result[1] = child[0].innerText;
                            }

				return result;


})();
