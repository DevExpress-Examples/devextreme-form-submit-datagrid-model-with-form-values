
@Code
    ViewBag.Title = "Success"
End Code

@Imports dxSampleT590924.Models
@ModelType Customer

<h2>Customer @Model.CustomerID has been suceessfully created</h2>
<div class="dx-fieldset">
    <div class="dx-fieldset-header">Details</div>
    <div class="dx-field">
        <div class="dx-field-label">First Name</div>
        <div class="dx-field-value">
            @Model.FirstName
        </div>
    </div>
    <div class="dx-field">
        <div class="dx-field-label">Last Name</div>
        <div class="dx-field-value">
            @Model.LastName
        </div>
    </div>
    <div class="dx-field">
        <div class="dx-field-label">Hire Date</div>
        <div class="dx-field-value">
            @Model.HireDate
        </div>
    </div>
</div>
<h2>Customer Orders:</h2>
@(Html.DevExtreme().DataGrid(Of Order)() _
                                                    .ID("grid") _
                                                    .ShowBorders(True) _
                                                    .DataSource(Model.Orders, "OrderID") _
                                                    .Editing(Sub(s)
                                                                 s.AllowUpdating(True).AllowAdding(True).AllowDeleting(True)
                                                             End Sub) _
                                                    .KeyExpr(New String() {"OrderID"}) _
                                        .Columns(Sub(columns)

                                                     columns.AddFor(Function(m) m.OrderID)
                                                     columns.AddFor(Function(m) m.OrderDate)
                                                     columns.AddFor(Function(m) m.CustomerName)
                                                     columns.AddFor(Function(m) m.ShipCountry)
                                                     columns.AddFor(Function(m) m.ShipCity)
                                                 End Sub) _
                                        .Paging(Function(p) p.PageSize(10))
)


<a class="navbar-brand" href="/">Back</a>