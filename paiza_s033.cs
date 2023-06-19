using System;
using System.Collections.Generic;

class Program
{
    static int GetMoves(int x_1, int y_1, int x_2, int y_2, int last_x_1, int last_y_1, int last_x_2, int last_y_2, List<List<bool>> map, int turns){

        var longest = int.MinValue;
        var next_x_2 = new List<int>();
        var next_y_2 = new List<int>();
        for(var i = -1; i<=1; i++){
            for(var j = -1; j<= 1;j++){
                if(Math.Abs(i) == Math.Abs(j))
                    continue;
                var next_x = x_2 + i;
                var next_y = y_2 + j;
                
                if(next_x < 0 || next_y < 0 || next_x >= map.Count || next_y >= map[0].Count)
                    continue;
                
                if(map[next_x][next_y] && (next_x != last_x_2 || next_y != last_y_2)){
                    next_x_2.Add(next_x);
                    next_y_2.Add(next_y);
                    
                }
            }
        }
        
        for(var i = -1; i<=1; i++){
            for(var j = -1; j<= 1;j++){
                if(Math.Abs(i) == Math.Abs(j))
                    continue;
                var next_x = x_1 + i;
                var next_y = y_1 + j;
                if(next_x < 0 || next_y < 0 || next_x >= map.Count || next_y >= map[0].Count)
                    continue;
                if(map[next_x][next_y] && (next_x != last_x_1 || next_y != last_y_1)){
                    if( next_x == x_2 && next_y == y_2)
                        return turns + 1;
                    var if_2_idle = Program.GetMoves(next_x, next_y, x_2, y_2, x_1, y_1, last_x_2, last_y_2, map, turns+1);
                    if (if_2_idle > longest){
                        longest = if_2_idle;
                    }
                    for(var k = 0; k < next_x_2.Count; k++){
                        var moving = Program.GetMoves(next_x, next_y, next_x_2[k], next_y_2[k], x_1, y_1, x_2, y_2, map, turns+1);
                        if (moving > longest){
                            longest = moving;
                        }
                    }
                }
            }
        }
        return longest;
    }
    
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        string[] line1 = Console.ReadLine().Trim().Split(' ');
        var columns_num = int.Parse(line1[0]);
        var lines_num = int.Parse(line1[1]);
        var map = new List<List<bool>>();
        int player1_x = 0;
        int player1_y = 0;
        int player2_x = 0;
        int player2_y = 0;
        for (var i = 0; i < lines_num; i++){
            var each_line = Console.ReadLine().Trim();
            map.Add(new List<bool>());
            for(var j = 0; j< columns_num; j++){
                var cell = each_line[j];
                if(cell == '#'){
                    map[i].Add(false);
                }else{
                    map[i].Add(true);
                    if(cell == '1'){
                        player1_x = i;
                        player1_y = j;
                    }else if (cell == '2'){
                        player2_x = i;
                        player2_y = j;
                    }
                }
            }
        }
        Console.WriteLine("{0}", Program.GetMoves(player1_x, player1_y, player2_x, player2_y, player1_x, player1_y, player2_x, player2_y, map, 0));
    }
}
