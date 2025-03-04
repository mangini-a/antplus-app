namespace WindowsFormsApp
{
    class Controller
    {
        private readonly double Kp;     // Proportional gain coefficient
        private readonly double Ki;     // Integral gain coefficient
        private readonly double Ts;     // Sampling time (how often control actions are computed, in seconds)

        private double error = 0;
        private double previousError = 0;
        private double integralTerm = 0;
        private readonly double setpoint;

        public double MaxOutput { get; set; } = 100;
        public double MinOutput { get; set; } = 1;

        /// <summary>
        /// Instantiates a PI controller with the provided setpoint, gains, and sampling frequency.
        /// </summary>
        /// <param name="setpoint"></param>
        public Controller(double setpoint, double kp = 0.75, double ki = 0, double ts = 15)
        {
            this.setpoint = setpoint;
            this.Kp = kp;
            this.Ki = ki;
            this.Ts = ts;
        }

        /// <summary>
        /// Calculates the control action depending on the last measured process variable.
        /// </summary>
        /// <param name="processVariable"></param>
        /// <returns></returns>
        public double ComputeControlAction(double processVariable)
        {
            error = setpoint - processVariable;         // Calculate current error
            integralTerm += Ki * Ts * previousError;    // Update integral term using previous error
            previousError = error;                      // Update previous error with current one

            // Calculate complete control action
            double output = Kp * error + integralTerm;

            // Apply limits and handle anti-windup
            if (output > MaxOutput)
            {
                output = MaxOutput;
                // If we're saturated high and error is positive, don't accumulate integral
                if (error > 0)
                    integralTerm -= Ki * Ts * error;
            }
            else if (output < MinOutput)
            {
                output = MinOutput;
                // If we're saturated low and error is negative, don't accumulate integral
                if (error < 0)
                    integralTerm -= Ki * Ts * error;
            }

            return output;
        }

        /// <summary>
        /// Resets the controller's state.
        /// </summary>
        public void Reset()
        {
            error = 0;
            previousError = 0;
            integralTerm = 0;
        }
    }
}
