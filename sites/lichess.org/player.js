(function(){
                           var div = $('.message');

                            if (div != null && div.length > 0) {
                                var child = div.children('div');
                                return child[0].innerText.indexOf('white') > -1 ? 'white':  'black';
                            }
                })();