@Imports dxSampleT590924.Models

@modelType Customer

@Code
    ViewBag.Title = "T590924"
End Code
<h2>Edit Form:</h2>

<script type="text/javascript" >
    function CreateInputElement(itemName, itemValue, itemIndex, container) {
        var $input = $("<input/>");

        $input.appendTo(container).attr({ type: "hidden", name: "Orders[" + itemIndex + "]." + itemName }).val(itemValue);
    }
    function clickHandler(e) {
        if (e.validationGroup.validate().isValid) {
            $("#grid").dxDataGrid("instance").getDataSource().load().done(function (items) {
                for (var i = 0; i < items.length; i++) {
                    var item = items[i];
                    for (var property in item) {
                        if (item.hasOwnProperty(property)) {
                            CreateInputElement(property, item[property], i, $("#editEmployee")[0]);
                        }
                    }
                }
                $("#editEmployee").submit();

            })
        }
    }
</script>

@Using (Html.BeginForm("PostCustomer", "Home", FormMethod.Post, New With {Key .id = "editEmployee"}))

    @Code 
        Html.DevExtreme().Form(Of Customer)() _
                            .ValidationGroup("customer") _
                            .FormData(Model) _
                            .Items(Sub(formItems)

            formItems.AddSimpleFor(Function(m) m.CustomerID) _
            .Editor(Function(e) e.NumberBox().ReadOnly(True))
            formItems.AddSimpleFor(Function(m) m.FirstName) _
.IsRequired(True)
            formItems.AddSimpleFor(Function(m) m.LastName) _
            .IsRequired(True)
            formItems.AddSimpleFor(Function(m) m.HireDate) _
            .Editor(Function(e) e.DateBox())
            formItems.AddSimple() _
.Template(Sub() _
                @<text>
            @(Html.DevExtreme().DataGrid(Of Order)() _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           .ID("grid") _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            .ShowBorders(True) _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            .Editing(Function(s) s.AllowUpdating(True).AllowAdding(True).AllowDeleting(True)) _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          .KeyExpr(New String() {"OrderID"}) _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          .DataSource(New JS("[]")) _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          .DateSerializationFormat("MM-dd-yyyy") _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          .Columns(Sub(columns)

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       columns.AddFor(Function(m) m.OrderID)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       columns.AddFor(Function(m) m.OrderDate)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       columns.AddFor(Function(m) m.CustomerName)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       columns.AddFor(Function(m) m.ShipCountry)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       columns.AddFor(Function(m) m.ShipCity)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   End Sub) _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   .Paging(Function(p) p.PageSize(10)) _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            .FilterRow(Function(f) f.Visible(True)) _
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            .HeaderFilter(Function(f) f.Visible(True)) _
                                                                                                                                                                                                                                                                                                                                                                .GroupPanel(Function(p) p.Visible(True)) _
                                                                                                                                                                                                                                                                                                                                                                .Grouping(Function(g) g.AutoExpandAll(False)) _
                                                                                                                                                                                                                                                                                                                                                                .RemoteOperations(False))
        </text>
                          End Sub)
                                   End Sub).Render()



    End Code

    @(Html.DevExtreme().Button() _
                                               .Text("Validate and Submit") _
                                               .ValidationGroup("customer") _
                                               .OnClick("clickHandler") _
                                               .UseSubmitBehavior(False) _
    )
        End Using 

@Html.ValidationSummary()
