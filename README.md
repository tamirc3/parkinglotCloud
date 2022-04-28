# ParkingLotApplication

Assignment 1 for cloud computing course Computer Science MSc Idc

## Prerequisite:
1.create a free subscription in Azure (https://azure.microsoft.com/en-us/free/)
2.download and install Azure CLI (https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli)

## how to run:
1.run the script "deployParkingLotApp.ps1"
The script will prompt to login to a Microsoft account,please use the account that you have a valid subscription.
next we will create a resource group called 'parkinglot-rg' within that an app service plan and a app service.
The last step will be to deploy our parking lot code from this repo into to the app service.

once the provisiong and deployment will be completed a health check will be done by calling a health api in the app service
(http://parkinglot-web-app-1.azurewebsites.net/health)
which will print to the console an OK response like the below:

StatusCode        : 200
StatusDescription : OK
Content           : app is up and running ,current datetime:04/28/2022 17:27:46

2.
In order to test the application you can use swagger to send an entry and exit calls 

http://parkinglot-web-app-1.azurewebsites.net/swagger

for using other options like postman:
for the entry request use the following  example body:
{
    "licensePlate" : "687212",
    "parkingLotID" : 5
}


for the exit request,place in the request body the ticketId as you got from the entry request, for example:43679555.


## Notes:
1.that after doing an exit the ticketID is deleted from the parking service<br>
2.the parking lot id should be a positive int

<br>
<br>
<br>




## The Exercise
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
