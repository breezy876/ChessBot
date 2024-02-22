(function(){
 			var div = document.querySelector('#player-bottom');

                            if (div != null) {
                                var child = div.querySelector('.move-time-dark');
                                if (child != null)
					return 'black';
				else
					return 'white';
                            }
                })();
