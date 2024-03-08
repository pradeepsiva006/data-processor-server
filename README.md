# data-processor-server

## APIs

### POST /parse-file

This API endpoint accepts a file, parses it, and returns the parsed data in the `DataItem` model format.

#### Request

- Method: POST
- Endpoint: /parse-file
- Body: Form data with the file to be parsed

#### Response

- Status Code: 200 OK
- Body: Parsed data in the `DataItem[]` format in JSON.

- Status Code: Error (400, 500)
- Body: JSON object with error details

### GET /updated-data

This API endpoint randomizes the most recent data and returns it to the client.

#### Request

- Method: GET
- Endpoint: /updated-data

#### Response

- Status Code: 200 OK
- Body: Randomized data in the `DataItem[]` format in JSON.

- Status Code: Error (400, 500)
- Body: JSON object with error details
- 
## Design Document

For detailed insights into the design considerations and decisions made for this project, please refer to the [Design Document] (https://github.com/pradeepsiva006/data-processor-server/blob/master/DESIGN-DOCUMENT.md).

Thank you!
