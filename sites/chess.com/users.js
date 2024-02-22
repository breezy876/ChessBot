 (function(){

var spans = document.getElementsByClassName('user-username-component');
return Array.from(spans, span => span.innerText);

})();