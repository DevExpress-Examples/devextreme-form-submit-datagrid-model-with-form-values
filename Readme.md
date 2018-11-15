<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/dxSampleT590924/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/dxSampleT590924/Controllers/HomeController.vb))
* [Order.cs](./CS/dxSampleT590924/Models/Order.cs) (VB: [Order.vb](./VB/dxSampleT590924/Models/Order.vb))
* **[Index.cshtml](./CS/dxSampleT590924/Views/Home/Index.cshtml)**
* [Success.cshtml](./CS/dxSampleT590924/Views/Home/Success.cshtml)
<!-- default file list end -->
# How to submit DataGrid model with form values


<p>By default, the HTML form collects information from all hidden inputs and posts it to the Controller. DataGrid is not an editor and doesn't have a hidden input. As a result, model values assigned to the grid's DataSource option are not passed to the server out of the box.</p>
<p>You can manually create hidden inputs for each row at runtime, place them onto the form and specify their name attribute as demonstrated in the "<a href="https://www.red-gate.com/simple-talk/dotnet/asp-net/model-binding-asp-net-core/">Model Binding in ASP.NET Core</a> -> Binding to Collections" help topic before it is submitted.</p>

<br/>


