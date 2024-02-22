(function(){
			    var users = ['',''];

                            var div = $('.ruser-top');
                            if (div != null && div.length > 0) {
                                var a = div.children('.user-link');
                                if (a != null && a.length > 0) {        
                                    users[0] = a[0].innerText;
                                }
                            }

          		    var div = $('.ruser-bottom');
                            if (div != null && div.length > 0) {
                                var a = div.children('.user-link');
                                if (a != null && a.length > 0) {        
                                    users[1] = a[0].innerText;
                                }
                            }

			return users;
                })();