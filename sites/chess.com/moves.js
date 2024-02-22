(function(){

 var turns = document.getElementsByClassName('move');

var moves = Array.from(turns,turn=>{ var parts = turn.innerText.substring(turn.innerText.indexOf('.')+1).split('\n'); return [parts[1],parts[3]]});
   
moves= moves.reduce(function(a,b) { return a.concat(b) }, []);


})();