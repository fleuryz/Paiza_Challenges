using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static int GetConections(List<int> dogs_invited, SortedList relationships, int max_lost_connections){
        List<int> monkeys_invited = new List<int>();
        for(var i = 0; i < ((List<int>)relationships[dogs_invited[0]]).Count; i++){
            var monkey_to_invite = ((List<int>)relationships[dogs_invited[0]])[i];
            var can_invite = true;
            for(var k = 1; k < dogs_invited.Count; k++){
                if(((List<int>)relationships[dogs_invited[k]]).Contains(monkey_to_invite)){
                    //((List<int>)relationships[dogs_invited[k]]).Remove(monkey_to_invite);
                }else{
                    can_invite = false;
                }
            }
            if(can_invite){
                monkeys_invited.Add(monkey_to_invite);
            }else{
                max_lost_connections--;
                if(max_lost_connections <= 0){
                    return 0;
                }
            }
        }
        
        return dogs_invited.Count + monkeys_invited.Count;
    }
    
    static int Get_Best_Number(List<int> dogs_invited, List<int> dogs_to_invite, int total_connections, SortedList relationships, int best_guest_number){
        if(dogs_to_invite.Count == 0){
            return best_guest_number;
        }
        int new_number;
        var new_dogs_invited = new List<int>(dogs_invited);
        var dog_to_add = -1;
        
        new_dogs_invited.Add(dogs_to_invite[0]);
        dogs_to_invite.RemoveAt(0);
        var new_max_connections = total_connections + ((List<int>)relationships.GetByIndex(0)).Count + 1;
        if( new_max_connections > best_guest_number){
            new_number = Program.GetConections(new_dogs_invited, relationships, best_guest_number-new_max_connections);
            best_guest_number = new_number > best_guest_number ? new_number : best_guest_number;
        }
        //Check with this dogs
            new_number = Get_Best_Number(new_dogs_invited, dogs_to_invite, new_max_connections, relationships, best_guest_number);
            best_guest_number = new_number > best_guest_number ? new_number : best_guest_number;
        //Check without this dog
            relationships.RemoveAt(0);
            new_number = Get_Best_Number(dogs_invited, dogs_to_invite, total_connections, relationships, best_guest_number);
            best_guest_number = new_number > best_guest_number ? new_number : best_guest_number;
            
            return best_guest_number;
    }
    
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        string[] initial_data = Console.ReadLine().Trim().Split(' ');
        var dogs_number = int.Parse(initial_data[0]);
        var monkeys_number = int.Parse(initial_data[1]);
        var relationships_number = int.Parse(initial_data[2]);
        var relationships = new SortedList();
        var dogs_to_invite = new List<int>();
        for(var i = 0; i< relationships_number; i++){
            string[] relationship_data = Console.ReadLine().Trim().Split(' ');
            var dog = int.Parse(relationship_data[0]);
            var monkey = int.Parse(relationship_data[1]);
            
            if(!relationships.ContainsKey(dog)){
                relationships.Add(dog, new List<int>());
            }
            ((List<int>)relationships[dog]).Add(monkey);
        }
        //A list sorted by the number of relationships is created
        var dogs_sorted = new SortedList();
        for(var i = 0; i< relationships.Count;i++){
            dogs_to_invite.Add((int)relationships.GetKey(i));
            var quantity = ((List<int>)relationships.GetByIndex(i)).Count;
            if(!dogs_sorted.Contains(quantity)){
                dogs_sorted.Add(quantity, new List<int>());
            }
            ((List<int>)dogs_sorted[quantity]).Add((int)relationships.GetKey(i));
        }
        
        var best_guest_number = dogs_number > monkeys_number ? dogs_number : monkeys_number;
        
        Console.WriteLine("{0}", Get_Best_Number(new List<int>(), dogs_to_invite, 0, relationships, best_guest_number));
    }
}
