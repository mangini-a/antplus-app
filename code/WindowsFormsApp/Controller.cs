namespace WindowsFormsApp
{
    class Controller
    {
        // Coefficients for the proportional and integral terms
        private const double Kp = 0.1;
        private const double Ki = 0.01;

        // Variables needed to implement a PI controller
        private int error = 0;
        private int previousError = 0;
        private double integralTerm = 0;
        private readonly byte setpoint;

        /// <summary>
        /// Instantiates a PI controller that operates on the provided setpoint.
        /// </summary>
        /// <param name="setpoint"></param>
        public Controller(byte setpoint)
        {
            this.setpoint = setpoint;
        }

        /// <summary>
        /// Calculates the control action depending on the measured process variable.
        /// </summary>
        /// <param name="processVariable"></param>
        /// <returns></returns>
        public double ComputeControlAction(byte processVariable)
        {
            error = setpoint - processVariable;
            integralTerm += previousError * Ki;     // Sampling time is equal to about one second
            previousError = error;

            return error * Kp + integralTerm;
        }
    }
}
