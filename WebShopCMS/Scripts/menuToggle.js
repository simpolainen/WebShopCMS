$(document).ready(function () {
$(function() {
    $("li.content").hide();
    $("ul.nav").delegate("li.toggle", "click", function() { 
        $(this).next().toggle("fast").siblings(".content").hide("fast");
        alert();
    });
});
});
