(function(){

  var result  = [null, null];
  var div = $('.message');

  result[0] = div != null && div.length > 0;
  result[1] = div[0].innerText.indexOf("your turn") > -1;

  return result;

})();
