(function(){

      var items = $('kwdb');

            if (items != null && items.length > 0)
            {
                var moves = Array.from(items, item => item.innerText);
                return moves;
            }

                return null;


	        })();