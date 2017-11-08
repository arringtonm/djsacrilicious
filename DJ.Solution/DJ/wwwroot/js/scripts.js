$(document).ready(function(){
    $(".item").each(function(){
        $(this).click(function() {
            var itemName = $(this).find(".event-name").text();
            var itemVenue = $(this).find(".venue-name").text();
            var itemAddress = $(this).find(".venue-address").text();

            $("#event-name").val(itemName);
            $("#venue-name").val(itemVenue);
            $("#venue-address").val(itemAddress);
        });
    });

    $(".edit-item").each(function(){
        $(this).click(function() {
            var itemName = $(this).find(".event-name").text();
            var itemVenue = $(this).find(".venue-name").text();
            var itemAddress = $(this).find(".venue-address").text();
            var itemId = $(this).find(".event-id").text();
            var itemStart = $(this).find(".event-start").text();
            var itemEnd = $(this).find(".event-end").text();

            $("#event-name").val(itemName);
            $("#venue-name").val(itemVenue);
            $("#venue-address").val(itemAddress);
            $("#event-start").val(itemStart);
            $("#event-end").val(itemEnd);
            alert(itemStart);
            $("form").attr("action", "/events/edit/" + itemId);
        });
    });
});
