using System;
using System.Collections.Generic;

class Position{
    public int X { get;set; }
    public int Y { get;set; }
    
    public Position(int x, int y){
        X = x;
        Y = y;
    }
    
    public override bool Equals(Object obj)
   {
      //Check for null and compare run-time types.
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else {
         Position p = (Position) obj;
         return (X == p.X) && (Y == p.Y);
      }
   }

   public override int GetHashCode()
   {
      return (X << 2) ^ Y;
   }

    public override string ToString()
    {
        return String.Format("({0}, {1})", X, Y);
    }
    
    public bool MoveTowards(Position piece){
        if (piece.X > X){
            X++;
        } else if(piece.X < X){
            X--;
        }
        
        if (piece.Y > Y){
            Y++;
        } else if(piece.Y < Y){
            Y--;
        }
        
        return (piece.X == X && piece.Y == Y);
    }
    
    public bool IsInvalid(int size, Position piece){
        return X < 0 || X >= size || Y < 0 || Y >= size || piece == this;
    }
    
    public Position Copy(){
        return new Position(X, Y);
    }
    
    
}

class Scenario{
    public Position piece_1 { get;set; }
    public Position piece_2 { get;set; }
    
    public Scenario(Position first, Position second){
        piece_1 = first;
        piece_2 = second;
    }
    
    public void Print(){
        Console.WriteLine("piece_1: {0}, piece_2: {1}", piece_1, piece_2);
    }
}

class Program
{
    static int GetBestPlay(Position piece_1, Position piece_2, int size){
        var turns = 0;
        
        var scenarios = new List<Scenario>();
        scenarios.Add(new Scenario(piece_1, piece_2));
        
        if(scenarios[0].piece_1.MoveTowards(scenarios[0].piece_2))
            scenarios.RemoveAt(0);
        turns++;
        
        while(scenarios.Count > 0){
            for(var i = scenarios.Count-1; i >= 0; i--){
                for(var j = -1; j<=1; j++){
                    for (var k = -1; k <= 1; k++){
                        if( k == 0 || j == 0 )
                            continue;
                        Position newPosition = new Position(scenarios[i].piece_2.X + j, scenarios[i].piece_2.Y + k);
                        if( !newPosition.IsInvalid(size, scenarios[i].piece_1)){
                            var new_scenario = new Scenario(scenarios[i].piece_1.Copy(), newPosition);
                            scenarios.Add(new_scenario);
                        }
                    }
                }
                
                scenarios.RemoveAt(i);
            }
            
            turns++;
            for(var i = scenarios.Count-1; i>=0; i--){
                
                if(scenarios[i].piece_1.MoveTowards(scenarios[i].piece_2)){
                    scenarios.RemoveAt(i);
                }
                    
            }
            
            
        }
        
        return turns;
    }
    
    
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        var size = int.Parse(Console.ReadLine().Trim());
        string[] line = Console.ReadLine().Trim().Split(' ');
        var piece_1 = new Position(int.Parse(line[0]), int.Parse(line[1]));
        
        line = Console.ReadLine().Trim().Split(' ');
        var piece_2 = new Position(int.Parse(line[0]), int.Parse(line[1]));
        
        Console.WriteLine("{0}", GetBestPlay(piece_1, piece_2, size));
    }
}
