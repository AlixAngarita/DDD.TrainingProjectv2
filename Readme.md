# Training event potential endpoints

**GET**    `/training-events/{id}`	Get a single training event
#### Response:
```json
200 OK
ETag: "5"

{
	"id": "34f34122-2227-4572-a2bc-2c2355f81afa6",
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
		{ "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", 
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

**POST**   `/training-events/{id}/instructors/batch-get` Get a list of instructors in a training event
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
      "assigneeId": "de802551-2c47-4323-9779-d74876aa0c3f",
      "etag": "16"
    }
  ],
  "notFound": [
    { "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7" }
  ]
}
```

**POST**   `/training-events/{id}/trainees/batch-get`	Get a list of trainees in a training event

Same example as `/training-events/{id}/instructors/batch-get`

**POST**   `/training-events/{id}/observers/batch-get`	Get a list of observers in a training event

Same example as `/training-events/{id}/instructors/batch-get`

**POST**   `/training-events/{id}/instructors/assign`	Assign one or multiple instructors to a training event
#### Request:
```json
{
  "items": [
    { "assigneeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6" }
  ]
}
```
#### Response:
```json
200 OK

{ 
	"content": [ 
		{ "id": "a0a43a72-fdda-4fe5-9034-ec2e0f84a0df", "assigneeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "etag": "8" } 
	] 
}
```

**POST**   `/training-events/{id}/trainees/assign`	Assign one or multiple trainees to a training event
#### Request:
```json
{
  "items": [
    { "assigneeId": "454c2121-4a06-4c61-bffd-717b035d77de" },
    { "assigneeId": "fe61665c-8dbb-49f8-9545-43c60824e409" },
    { "assigneeId": "7ea17f16-12fa-408a-9cc3-292448a50f67" }
  ]
}
```
#### Response:
```json
200 OK

{ 
	"content": [ 
		{ "id": "a0a43a72-fdda-4fe5-9034-ec2e0f84a0df", "assigneeId": "454c2121-4a06-4c61-bffd-717b035d77de", "etag": "3" },
		{ "id": "9d7fb320-38ed-441c-9bb7-10fd78f508d5", "assigneeId": "fe61665c-8dbb-49f8-9545-43c60824e409", "etag": "9" },
		{ "id": "ff15434d-e12a-43fe-936d-665de3c58532", "assigneeId": "7ea17f16-12fa-408a-9cc3-292448a50f67", "etag": "4" }
	] 
}
```

**POST**   `/training-events/{id}/observers/assign` Assign one or multiple observers to a training event
Same example as `/training-events/{id}/trainees/assign`

**POST**   `/training-events/{id}/instructors/unassign`	Unassign one or multiple instructors to a training event
#### Request:
```json
{
  "items": [
    { "id": "a0a43a72-fdda-4fe5-9034-ec2e0f84a0df", "etag": "3" },
    { "id": "9d7fb320-38ed-441c-9bb7-10fd78f508d5", "etag": "9" },
    { "id": "ff15434d-e12a-43fe-936d-665de3c58532", "etag": "4" }
  ]
}
```
#### Response:
```json
200 OK

{
  "content": [
    { "id": "a0a43a72-fdda-4fe5-9034-ec2e0f84a0df" },
    { "id": "9d7fb320-38ed-441c-9bb7-10fd78f508d5" }
  ],
  "failed": [
    { "id": "ff15434d-e12a-43fe-936d-665de3c58532" }
  ]
}
```

**POST**   `/training-events/{id}/trainees/unassign`	Unassign one or multiple trainees to a training event

Same example as `/training-events/{id}/instructors/unassign`

**POST**   `/training-events/{id}/observers/unassign`	Unassign one or multiple observers to a training event

Same example as `/training-events/{id}/instructors/unassign`
