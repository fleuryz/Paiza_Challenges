using System;
using System.Collections.Generic;

class Tile{
    public int color { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public Tile(int color, int x, int y){
        this.color = color;
        this.x = x;
        this.y = y;
    }
}

class FloorPlan{
    public int size { get; set; }
    public int tile_size { get; set; }
    public int color_num { get; set; }

    public FloorPlan(int size, int tile_size, int color_num){
        this.size = size;
        this.tile_size = tile_size;
        this.color_num = color_num;
    }
    
    public List<List<int>> EmptyFloor(){
        var return_floor = new List<List<int>>();
        for(var i = 0; i<size; i++){
            return_floor.Add(new List<int>());
            for(var j = 0; j<size;j++){
                return_floor[i].Add(0);
            }
        }
        return return_floor;
    }
    
    public List<List<int>> MakeFloor(List<Tile> tiles){
        var return_floor = this.EmptyFloor();
        
        foreach(Tile tile in tiles){
            for(var i = tile.y; i < tile.y + tile_size; i++){
                for (var j = tile.x; j<tile.x + tile_size;j++){
                    return_floor[i][j] = tile.color;
                }
            }
        }
        
        /*for(var i = 0 ; i< size; i++){
            for (var j = 0; j < size; j++){
                Console.Write("{0}", return_floor[i][j]);
            }
            Console.Write("\n");
        }
        Console.Write("\n");*/
        
        return return_floor;
    }
    
    public bool EqualFloor( List<List<int>> first_floor, List<List<int>> second_floor){
        for(var i = 0; i<size; i++){
            for(var j = 0; j<size;j++){
                if(first_floor[i][j] != second_floor[i][j])
                    return false;
            }
        }
        
        return true;
    }
    
}

class Program
{
    static List<Tile> MakeAllFloors(List<List<int>> floor_result, FloorPlan floor_plan){
        var tiles = new List<Tile>();
        var colors_used = new List<int>();
        for(var i = 0; i < floor_plan.color_num; i++){
            tiles.Add(new Tile(0,0,0));
        }
        
        CheckFloor(tiles, colors_used, floor_plan, 0, floor_result);
        
        return tiles;
    }
    
    static bool CheckFloor(List<Tile> tiles, List<int> colors_used, FloorPlan floor_plan, int current_spot, List<List<int>> return_floor){
        if(current_spot >= floor_plan.color_num){
            return floor_plan.EqualFloor(floor_plan.MakeFloor(tiles), return_floor);
        }
        
        for(var i = 1; i <= floor_plan.color_num; i++){
            if(colors_used.Contains(i)){
                continue;
            }else{
                colors_used.Add(i);
                for(var j = 0; j <= floor_plan.size - floor_plan.tile_size;j++){
                    for(var k = 0; k <= floor_plan.size - floor_plan.tile_size;k++){
                        if(return_floor[j][k] == 0 
                            || return_floor[j+floor_plan.tile_size-1][k+floor_plan.tile_size-1]==0
                            || (return_floor[j][k] != i && colors_used.Contains(return_floor[j][k]))
                            ){
                            continue;
                        }
                        tiles[current_spot].color = i;
                        tiles[current_spot].x = k;
                        tiles[current_spot].y = j;
                        
                        //Console.WriteLine("order {0}, color {1}, position ({2},{3})", current_spot, i, j, k);
                        if(Program.CheckFloor(tiles, colors_used, floor_plan, current_spot+1, return_floor)){
                            //Console.WriteLine("ACHOOOOOOOOOOOU");
                            return true;
                        }
                    }
                }
                colors_used.Remove(i);
            }
        }
        
        return false;
                        
    }
    
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        string[] first_line = Console.ReadLine().Trim().Split(' ');
        var floor_size = int.Parse(first_line[0]);
        var tile_size = int.Parse(first_line[1]);
        var color_num = int.Parse(first_line[2]);
        var floor_result = new List<List<int>>();
        
        for(var i = 0 ; i< floor_size; i++){
            floor_result.Add(new List<int>());
            string each_line = Console.ReadLine().Trim();
            for (var j = 0; j < floor_size; j++){
                var tile = each_line[j] == '.' ? 0 : int.Parse(each_line[j].ToString());
                floor_result[i].Add(tile);
            }
        }
        var floor_plan = new FloorPlan(floor_size, tile_size, color_num);
        
        var tiles = MakeAllFloors(floor_result, floor_plan);
        
        foreach(Tile tile in tiles){
            Console.WriteLine("{0} {1} {2}", tile.color, tile.x+1, tile.y+1);
        }
    }
}
