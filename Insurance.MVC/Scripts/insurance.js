$(document).ready(function () {
    $('#btnSearch').on('click', function (e) {

        if ($('#iFirstName').val() !== '' || $('#iLastName').val() !== '') {
            e.preventDefault();
            var searchFilter = {
                FirstName: $('#iFirstName').val(),
                LastName: $('#iLastName').val(),
                QuoteId: $('#Quote_QuoteId').val()
            };

            $.ajax({
                type: "POST",
                url: "/Home/SearchPeople",
                datatype: "html",
                data: searchFilter,
                success: function (Data) {
                    $("#searchData").html(Data);
                    $("#searchData tr:nth-child(even)").addClass("SearchDataRow");
                    $("#searchData td").addClass("SearchDataCell");
                    $("#searchData").show();

                    if ($("#searchData tr").length == 1) {
                        $("#noRecords").show();
                    }
                    else {
                        $("#noRecords").hide();
                    }
                }
            });
            $("#warningMsg").hide();
            $("#iFirstName").removeClass("warningColor");
            $("#iLastName").removeClass("warningColor");
        }
        else {
            $("#warningMsg").show();
            $("#iFirstName").addClass("warningColor");
            $("#iLastName").addClass("warningColor");
        }

    });
    $("#searchData").hide();
    $("#warningMsg").hide();
    $("#noRecords").hide();
});

function GetAdditionalInsured(applicationInsuredId, personId, quoteId) {
    var additionalInsured = {
        AdditionalInsuredId: applicationInsuredId,
        QuoteId: quoteId,
        PersonId: personId
    };

    $.ajax({
        type: "POST",
        url: "/Home/AddPeople",
        datatype: "html",
        data: additionalInsured,
        success: function (Data) {
            $("#AdditionalInsuredTable").html(Data);
        }
    });
}

function RemoveAdditionalInsured(applicationInsuredId, personId, quoteId) {
    var additionalInsured = {
        AdditionalInsuredId: applicationInsuredId,
        QuoteId: quoteId,
        PersonId: personId
    };

    $.ajax({
        type: "POST",
        url: "/Home/RemovePeople",
        datatype: "html",
        data: additionalInsured,
        success: function (Data) {
            $("#AdditionalInsuredTable").html(Data);
        }
    });
}

