## Key classes
The main classes building up the implemented solution are listed.

### Form1.cs
Represents the window making up the application's user interface, which also takes care of configuring the ANT+ network and acquiring data during workouts with the help of the involved hardware.  
Creates an object of the two classes mentioned below at the appropriate time.

### DataManager.cs
Stores sensor data in a dictionary holding it along with the timestamp (in ms) at which it is received, to then iterate through the data dictionary and write each row to a CSV file at the end of the training session.

### Controller.cs
Instantiates a PI controller with the provided setpoint, gains, and sampling time when the workout begins, which will thereafter be able to calculate the needed control action (i.e., the resistance the trainer shall apply) depending on the last detected heart rate value.
