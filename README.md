# parkinglotCloud
ParkingLotApplication
Assignment 1 for cloud computing course Computer Science MSc Idc

Exercise 1 - Parking Lot The scenario: • Build a cloud-based system to manage a parking lot.

• Camera will recognize license plate and ping cloud service

• Actions:

• Entry (record time, license plate and parking lot)

• Exit (return the charge for the time in the parking lot)

• Price – 10$ per hour (by 15 minutes)

Endpoints:

You need to implement two HTTP endpoints:

• POST /entry?plate=123-123-123&parkingLot=382

o Returns ticket id

• POST /exit?ticketId=1234

o Returns the license plate, total parked time, the parking lot id and the charge (based on 15 minutes increments).

The task:

Build a system that would track and compute cars entry & exit from parking lots, as well as compute their charge. The system should be deployed to AWS in one of two ways:

As a serverless solution, covered in Lesson 4.

Deployed on an EC2 instance as standard application, covered in Lesson 3.

Notes:

• You may use any technology stack that you’ll like (Node.js, Python, C#, JVM, etc).

• Data persistence is left at your discretion, we’ll cover persistence in the cloud in Lesson 5.

You need to create:

• The code that would handle the above-mentioned endpoints.

• Include a script that would deploy the code to the cloud. Can be bash, cloud formation, custom code, etc.

• Upload the results (OneDrive, S3, GitHub, Google Drive, etc) and provide link to the code.

• Inclusion of access keys in the submission will automatically reduce 25% of the grade.
