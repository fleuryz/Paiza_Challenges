using System;

class Program
{
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        string[] stArrayData = Console.ReadLine().Trim().Split(' ');
        double x_size = double.Parse(stArrayData[0]);
        double y_size = double.Parse(stArrayData[1]);
        double current_x = double.Parse(stArrayData[2]);
        double current_y = double.Parse(stArrayData[3]);
        double radius = double.Parse(stArrayData[4]);
        double degrees = double.Parse(stArrayData[5]);
        double l_value = double.Parse(stArrayData[6]);
        
        double angle    = Math.PI * degrees / 180.0;
        double x_travel = l_value*Math.Cos(angle);
        double y_travel = l_value*Math.Sin(angle);
        
        //Travel can be divided by each axis independently
        //Travel will be reduced until the ball can't travel anymore
        
        while(Math.Abs(x_travel) > Double.Epsilon){
            if(x_travel > 0){
                //If the ball is going to the right
                if ((x_travel + current_x + radius) > x_size){
                    //If the ball is going until the end of the pool and should still travel
                    x_travel -= x_size - (current_x + radius);
                    current_x = x_size - radius;
                    //Now the ball will go back
                    x_travel = -x_travel;
                }else{
                    //If the ball is going to stop before or at the end of the pool
                    current_x += x_travel;
                    x_travel = 0;
                }
            }else if (x_travel < 0){
                //If the ball is going to the left
                if ((x_travel + current_x - radius) < 0){
                    x_travel += (current_x - radius);
                    current_x = radius;
                    x_travel = -x_travel;
                }else{
                    current_x += x_travel;
                    x_travel = 0;
                }
            }
        }
        
        //The same is repeated for the Y axis
        while(Math.Abs(y_travel) > Double.Epsilon){
            if(y_travel > 0){
                //If the ball is going up
                if ((y_travel + current_y + radius) > y_size){
                    y_travel -= y_size - (current_y + radius);
                    current_y = y_size - radius;
                    y_travel = -y_travel;
                }else{
                    current_y += y_travel;
                    y_travel = 0;
                }
            }else if (y_travel < 0){
                //If the ball is going down
                if ((y_travel + current_y - radius) < 0){
                    y_travel += (current_y - radius);
                    current_y = radius;
                    y_travel = -y_travel;
                }else{
                    current_y += y_travel;
                    y_travel = 0;
                }
            }
        }
        
        Console.WriteLine("{0} {1}", current_x, current_y);
    }
}
