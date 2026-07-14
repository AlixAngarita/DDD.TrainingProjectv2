# Training event potential endpoints

**GET**    `/training-events/{id}`	Get a single training event
#### Response:
```json
200 OK
ETag: "5"

{
	"id": "34f34122-2227-4572-a2bc-2c2355f81af6",
	"startDate": "2026-12-01T09:00:00Z", 
	"endDate": "2026-12-12T11:00:00Z",
	"name": "A320 Training",
	"projectId": "de802551-2c47-4323-9779-d74876aa0c3f",
	"status": "Confirmed"
}
```

**POST**   `/training-events/batch-get` Get multiple training events
#### Request:
```json
{ 
	"items": [ 
		{ "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6" }, 
		{ "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7" } 
	] 
}
```
#### Response:
```json
200 OK

{
    "content": [
        {
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", 
            "startDate": "2026-10-10T14:00:00Z", 
            "endDate": "2026-10-10T16:00:00Z", 
            "name": "A320 Training", 
            "projectId": "de802551-2c47-4323-9779-d74876aa0c3f", 
            "status": "Confirmed", 
            "etag": "12"
        }
    ],
    "notFound": [
        { "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7" }
    ]
}
```

**POST**   `/training-events` Create a new single training event
#### Request:
```json
{
	"projectId": "de802551-2c47-4323-9779-d74876aa0c3f",
	"name": "A320 Training - Session 1",
	"startDate": "2026-12-01T09:00:00Z", 
	"endDate": "2026-12-12T11:00:00Z"
}
```
#### Response:
```json
201 Created

{
	"id": "a0a43a72-fdda-4fe5-9034-ec2e0f84a0f6"
}
```

**PUT**   `/training-events/{id}` Modify an existing training event
#### Request:
```json
If-Match: "5"
{
	"name": "A320 Training - First session"
}
```
#### Response:
```json
200 OK
ETag: "6"
```

**DELETE**   `/training-events/{id}` Delete an existing training event
#### Request:
```json
If-Match: "5"
```
#### Response:
```json
204 No Content
```

**POST**   `/training-events/{id}/cancel` Cancel a training event
#### Request:
```json
If-Match: "5"
```
#### Response:
```json
200 OK
ETag: "8"
```

**GET**   `/training-events/{id}/instructors` Get the list of instructors for a training event
#### Response:
```json
200 OK

{
  "content": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "assigneeId": "de802551-2c47-4323-9779-d74876aa0c3f",
      "etag": "16"
    }
  ]
}
```

**GET**   `/training-events/{id}/trainees`	Get the list of trainees for a training event

Same example as `/training-events/{id}/instructors`

**GET**   `/training-events/{id}/observers`	Get the list of observers for a training event

Same example as `/training-events/{id}/instructors`

**POST**   `/training-events/{id}/instructors`	Assign one instructor to a training event
#### Request:
```json
{
  "assigneeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
#### Response:
```json
201 Created

{
  "id": "a0a43a72-fdda-4fe5-9034-ec2e0f84a0df", 
  "assigneeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

**POST**   `/training-events/{id}/trainees`	Assign one trainee to a training event
#### Request:
```json
{
  "assigneeId": "454c2121-4a06-4c61-bffd-717b035d77de"
}
```
#### Response:
```json
201 Created

{
  "id": "a0a43a72-fdda-4fe5-9034-ec2e0f84a0df", 
  "assigneeId": "454c2121-4a06-4c61-bffd-717b035d77de"
}
```

**POST**   `/training-events/{id}/observers` Assign one observer to a training event
Same example as `/training-events/{id}/trainees`

**DELETE**   `/training-events/{id}/instructors/{instructorId}`	Unassign one instructor from a training event
#### Response:
```json
204 No content
```

**DELETE**   `/training-events/{id}/trainees/{traineeId}`	Unassign one trainee from a training event

Same example as `/training-events/{id}/instructors/{instructorId}`

**DELETE**   `/training-events/{id}/observers/{observerId}`	Unassign one observer from a training event

Same example as `/training-events/{id}/instructors/{instructorId}`

**POST**   `/training-events/create-and-update`	Create a training event and execute commands to create and update entities
#### Request:
```json
{
  "projectId": "de802551-2c47-4323-9779-d74876aa0c3f",
  "name": "A320 Training - Session 1",
  "startDate": "2026-12-01T09:00:00Z",
  "endDate": "2026-12-12T11:00:00Z",
  "commands": [
    {
      "assignInstructor": {
        "instructor": { "assigneeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6" }
      }
    },
    {
      "assignTrainee": {
        "trainee": { "assigneeId": "d2c2bbd1-d883-462a-8155-a6e6d786ae1c" }
      }
    }
  ]
}
```
#### Response:
```json
200 OK

{ 
  "responses": [
    { "added" : [
      { "id": "a0a43a72-fdda-4fe5-9034-ec2e0f84a0df" },
      { "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7"} ]
    },
    { "updated": { "id": "454c2121-4a06-4c61-bffd-717b035d77de"} }
  ]
}
```

**POST**   `/training-events/{id}/batch-update/all-or-none` Perform multiple updates in the training event aggregate
#### Request:
```json
If-Match: "13"
{
  "commands": [
    {
      "unassignTrainee": { 
        "trainee": { "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6" }
      }
    },
    {
      "unassignTrainee": {
        "trainee": { "id": "eadb8f24-9e02-4109-a93a-2e29b7de644c" }
      }
    },
    {
      "unassignTrainee": {
        "trainee": { "id": "d2c2bbd1-d883-462a-8155-a6e6d786ae1c" }
      }
    }
  ]
}
```
#### Response:
```json
200 OK
ETag: "16"

{ 
  "responses": [
    { "added" : { } },
    { "updated" : { } },
    { "removed": [
      { "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6" },
      { "id": "eadb8f24-9e02-4109-a93a-2e29b7de644c" },
      { "id": "d2c2bbd1-d883-462a-8155-a6e6d786ae1c" }] 
    }
  ]
}
```