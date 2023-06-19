using System;
using System.Collections.Generic;

class City{
    public int x;
    public int y;
    
    public City(int x, int y){
        this.x = x;
        this.y = y;
    }
    
    
}

class Program
{
    public static int GetDistance(int city_1, int city_2, List<City> cities, List<List<int>> distance_map, int height_map, int width_map){
        if(distance_map[city_1][city_2] < int.MaxValue){
            return distance_map[city_1][city_2];
        }else if(distance_map[city_2][city_1] < int.MaxValue){
            return distance_map[city_2][city_1];
        }
        var difference_x = Math.Abs(cities[city_1].x - cities[city_2].x);
        var width_map_half = (int)Math.Floor((float)(width_map/2));
        var distance_x = difference_x <= width_map_half ? difference_x : width_map - difference_x;
        
        var difference_y = Math.Abs(cities[city_1].y - cities[city_2].y);
        
        int height_map_half = (int)Math.Floor((float)(height_map/2));
        var distance_y = difference_y <= height_map_half ? difference_y : height_map - difference_y;
        
        return distance_x + distance_y;
    }
    
    public static int GetNShortestIndexFor(int city, int shortest_n,  List<List<int>> distance_map){
        SortedList<float,int> sorted_list = new SortedList<float,int>();
        for(int i =0; i< distance_map[city].Count; i++){
            float to_add = (float)distance_map[city][i];
            while(sorted_list.ContainsKey(to_add)){
                to_add+=0.01f;
            }
            sorted_list.Add(to_add, i);
        }
        
        return sorted_list.Values[1+shortest_n];
    }
    
    public static int GetShortestDistance(List<int> visited_cities, List<int> unvisited_cities, List<List<int>> distance_map){
        if(unvisited_cities.Count <= 0){
            return 0;
        }
        var shortest_distance = int.MaxValue;
        var city_to_travel = -1;
        
        
        foreach(int city in visited_cities){
            var i = 0;
            var city_to_test = -1;
            
            do{
                city_to_test = Program.GetNShortestIndexFor(city, i, distance_map); 
                i++;
            }while(visited_cities.Contains(city_to_test));
            if(distance_map[city][city_to_test] < shortest_distance){
                shortest_distance = distance_map[city][city_to_test];
                city_to_travel = city_to_test;
            }
        }
        visited_cities.Add(city_to_travel);
        unvisited_cities.Remove(city_to_travel);
        
        return shortest_distance + GetShortestDistance(visited_cities, unvisited_cities, distance_map);
    }
    
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        string[] first_line = Console.ReadLine().Trim().Split(' ');
        var cities_num = int.Parse(first_line[0]);
        var height_map = int.Parse(first_line[1]);
        var width_map = int.Parse(first_line[2]);
        var cities = new List<City>();
        var distance_map = new List<List<int>>();
        
        for(var i = 0; i<cities_num; i++){
            string[] line = Console.ReadLine().Trim().Split(' ');
            cities.Add(new City(int.Parse(line[0]), int.Parse(line[1])));
            distance_map.Add(new List<int>());
            for(var j = 0; j < cities_num; j++){
                if(i == j){
                    distance_map[i].Add(0);    
                }else{
                    distance_map[i].Add(int.MaxValue);
                }
            }
        }
        
        List<int> cities_to_visit = new List<int>();
        for(var i = 0; i<cities_num; i++){
            for(var j = 0; j < cities_num; j++){
                if(i != j){
                    var distance = Program.GetDistance(i,j, cities, distance_map, height_map, width_map);
                    distance_map[i][j] = (distance);
                }
            }
            cities_to_visit.Add(i);
        }
        
        var shortest_distance = int.MaxValue;
        for(int i = 0; i< cities_num; i++){
            cities_to_visit.Remove(i);
            var current_distance = Program.GetShortestDistance(new List<int>(){i}, new List<int>(cities_to_visit), distance_map);

            cities_to_visit.Add(i);
            
            if(current_distance < shortest_distance){
                shortest_distance = current_distance;
            }
        }
        
        Console.WriteLine("{0}", shortest_distance);
        
        
    }
}
