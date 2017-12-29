$(document).ready(function () {
    UserBookGrid();
});

function UserBookGrid() {
    $("#userbookcollection").ejGrid({
        dataSource: bookdetailsList,
        allowSelection: false,
        allowSorting: true,
        contextMenuSettings: { enableContextMenu: false },
        allowFiltering: true,
        filterSettings: { filterType: "excel" },
        allowResizing: true,
        allowEditing: false,
        allowScrolling: false,
        enableTouch: false,
        load: window.GridToolTipBehavior,
        enableAltRow: true,
        allowPaging: true,
        pageSettings: { pageSize: 50 },
        toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Search] },
        actionBegin: function (args) {
            if (args.requestType == "filtering" && args.currentFilterObject[0].operator == "startswith")
                args.currentFilterObject[0].operator = "contains";
            this.model.query._params.push({ key: "$id", value: "" });
        },
        actionComplete: function (args) {
            if (args.model.currentViewData != null && args.model.currentViewData.length != 0) {
                $("#userbook-export").css("display", "block");
            }
            else {
                $("#userbook-export").css("display", "none");
            }
        },
        columns: [
                { field: "BookId", headerText: "Book ID", width: 32 },
                { headerText: "Book Name", field: "BookName", width: 45 },
                { headerText: "Book Category", field: "CategoryName", width: 32 },
                { headerText: "Author", field: "AuthorName", width: 35 },
                { headerText: "Publisher", field: "Publisher", width: 35 },
                { headerText: "ISBN No", field: "ISBNNumber", width: 35 },
                { headerText: "Total No of Copies", field: "NoOfCopies", width: 35 },                
                { headerText: "Requested Copies", field: "RequestedCopies", width: 34 },
                { headerText: "Available Copies", field: "AvailableCopies", width: 34 },
                { headerText: "Action", width: 34 , template: "<a href='' id='bookRequest' onclick ='SendBookRequest({{:BookId}})' >Send Request</a>"},
        ],
    });
}

function SendBookRequest(bookId)
{
    var data = '{"BookId":"' + bookId + '", "UserId":"' + $("#UserId").text() + '", "Comments":"' + "" + '"}';
    $.ajax({
        type: "POST",
        url: "/books/sendbookrequest",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result == true)
            {
                UserBookGrid();
            }
        }
    });
}
