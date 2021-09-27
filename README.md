# C-Sharp-Live-Project
My contribution to a ASP.NET site for Theatre Vertigo. I created a section with CRUD pages for managing rental requests. It connects to a code-first database with Entity Framework. Bootstrap used for styling and jQuery for filtering expired rental requests on index page.
 
![image](https://user-images.githubusercontent.com/84836870/134958891-647e4fb7-7a4e-4f95-a982-39d7d5fc2ac2.png)

From RentalRequestModel.cs:
```csharp
public class RentalRequest
{
    public int RentalRequestID { get; set; }
    public string ContactPerson { get; set; }
    public string Company { get; set; }
    public DateTime RequestedTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string ProjectInfo { get; set; }
    public int RentalCode { get; set; }
    public bool Accepted { get; set; }
    public bool ContractSigned { get; set; }
    public bool IgnoreSurveyPrompt { get; set; }
}
```

Sort rental requests by date from RentalRequestsController.cs:
```csharp
public ActionResult Index()
{
    return View(db.RentalRequests.OrderBy(i => i.StartTime).ToList());
}
```

Accordion display for active requests from Index.cshtml:
```csharp
@{int i = 0;}
<div class="accordion" id="rr-active-requests">
@foreach (var item in Model)
{
    string startTimeShortTime = item.StartTime.ToShortTimeString();
    string startTimeLongDate = item.StartTime.ToLongDateString();
    string endTimeShortTime = item.EndTime.ToShortTimeString();
    string endTimeLongDate = item.EndTime.ToLongDateString();

    TimeSpan timeTillStart = (item.StartTime - DateTime.Now);
    string timeTillStartString = $"{timeTillStart.ToString("%d")} days, {timeTillStart.ToString("%h")} hours, {timeTillStart.ToString("%m")} minutes";

    TimeSpan timeRemaining = (item.EndTime - DateTime.Now);
    string timeRemainingString = $"{timeRemaining.ToString("%d")} days, {timeRemaining.ToString("%h")} hours, {timeRemaining.ToString("%m")} minutes";

    bool expired = item.EndTime < DateTime.Now;

    if (!expired)
    {
        <div class="card">
            <div class="card-header rr-header">
                <button class="btn btn-block text-left" type="button" data-toggle="collapse" data-target="#request_@i" aria-expanded="false" aria-controls="request_@i">
                    <h1 class="mb-0">@Html.DisplayFor(modelItem => item.Company)</h1>
                    <h2>@Html.DisplayFor(modelItem => item.ContactPerson)</h2>
                    @{
                        if (DateTime.Now < item.StartTime)
                        {
                            <h3 class="rr-time-start-remaining">Time Until Start: @timeTillStartString</h3>
                        }
                        else
                        {
                            <h3 class="rr-time-start-remaining">Time Remaining: @timeRemainingString</h3>
                        }
                    }
                </button>
            </div>

            <div class="collapse" id="request_@i">
                <div class="card-body rr-body">
                    <div class="rr-request-times">
                        <div class="rr-request-start-time">
                            <h3>Start Time</h3>
                            <h4>
                                @Html.DisplayFor(modelItem => startTimeShortTime)
                            </h4>
                            <h5>
                                @Html.DisplayFor(modelItem => startTimeLongDate)
                            </h5>
                        </div>
                        <div class="rr-request-end-time">
                            <h3>End Time</h3>
                            <h4>
                                @Html.DisplayFor(modelItem => endTimeShortTime)
                            </h4>
                            <h5>
                                @Html.DisplayFor(modelItem => endTimeLongDate)
                            </h5>
                        </div>
                    </div>

                    <p><b>Rental Code: </b>@Html.DisplayFor(modelItem => item.RentalCode)</p>
                    <p><b>Project Info: </b>@Html.DisplayFor(modelItem => item.ProjectInfo)</p>
                    <p><b>Rentals: </b></p>
                    <p><b>Accepted: </b>@Html.DisplayFor(modelItem => item.Accepted)</p>
                    <p><b>Contract Signed: </b>@Html.DisplayFor(modelItem => item.ContractSigned)</p>
                    <p><b>Ignore Survey? </b>@Html.DisplayFor(modelItem => item.IgnoreSurveyPrompt)</p>
                    @if (DateTime.Now >= item.EndTime.AddDays(-1) && DateTime.Now <= item.EndTime.AddDays(7))
                    {
                        <button>Fill out a rental survey</button>
                        <button>Ignore Survey</button>
                    }

                    @*@if (DateTime.Now > item.EndTime.AddDays(7))
                    {
                        item.IgnoreSurvey();
                    }*@

                    @Html.ActionLink("Edit", "Edit", new { id = item.RentalRequestID }) |
                    @Html.ActionLink("Details", "Details", new { id = item.RentalRequestID }) |
                    @Html.ActionLink("Delete", "Delete", new { id = item.RentalRequestID })
                </div>
            </div>
        </div>
        i++;
    }
}
</div>
```
