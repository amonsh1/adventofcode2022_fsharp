module AdventOfCode2022.Main


let main () = 
    assert (Day1.task_1("src/day1/input") = 69836)
    assert (Day1.task_2("src/day1/input") = 207968)

    assert (Day2.task_1("src/day2/input") = 15337)
    assert (Day2.task_2("src/day2/input") = 11696)
    
main()