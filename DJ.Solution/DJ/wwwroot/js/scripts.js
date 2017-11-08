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

            $("#event-name").val(itemName);
            $("#venue-name").val(itemVenue);
            $("#venue-address").val(itemAddress);
            $("form").attr("action", "/events/edit/" + itemId);
        });
    });
});
