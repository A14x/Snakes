Module Module1
    Dim worm(99, 99) As Integer        'ised to track worm segments in arena
    Dim arena(49, 23) As Char          'used to recrd wjhat isa in the artena x,y
    Dim highScoreName(9) As String     'hgihbf safsscioor stable navel
    Dim highScoreValue(9) As Integer   'high score table scires
    Dim wormHeadPointer As Integer     'record current head of the snake
    Dim wormTailPointer As Integer     'record tail lockation
    Dim wormDirection As Char
    Dim wormAlive As Boolean
    Dim wormGrow As Boolean
    Dim gameSpeed As Decimal
    Dim score As Integer
    Dim foodTimer As Integer
    Dim pause As Boolean
    Dim xFood, yFood As Byte
    Dim bonusLevel As Boolean = False
    Dim bonusLevelDone As Boolean = False
    Dim bonusFruitCount As Int16 = 0
    Dim bonusLevelScore As Int16 = 1000
    Dim difficulty As Decimal
    Dim menuOption As Byte

    Const forever As Boolean = False

    '##################################################### 🍏
    'proceeduyre to derop food iuntoew arena
    Sub drop_food()

        Do
            xFood = Rnd() * 49
            yFood = Rnd() * 23
        Loop Until (arena(xFood, yFood) = " ")
        arena(xFood, yFood) = "+"
        Console.SetCursorPosition(xFood, yFood)
        Console.Write("+")
        foodTimer = 200
    End Sub

    '#####################################################
    Sub initialise_game()
        Dim counterX, counterY As Byte
        'initialiase the arena
        For counterX = 0 To 49
            For counterY = 0 To 23
                arena(counterX, counterY) = " "
            Next
        Next

        'sets colour to black
        Console.BackgroundColor = 0

        'init game vars
        gameSpeed = 70
        score = 0
        wormAlive = True
        wormGrow = False
        wormHeadPointer = 4
        wormTailPointer = 1
        wormDirection = "r"
        worm(1, 1) = 5
        worm(1, 2) = 5
        arena(5, 5) = "#"
        worm(2, 1) = 6
        worm(2, 2) = 5
        arena(6, 5) = "#"
        worm(3, 1) = 7
        worm(3, 2) = 5
        arena(7, 5) = "#"
        worm(4, 1) = 8
        worm(4, 2) = 5
        arena(8, 5) = "#"
        difficulty = 0
        menuOption = 0
        bonusLevel = False
        bonusLevelDone = False
        Console.CursorVisible = False
        Randomize()
        drop_food()



    End Sub

    '#####################################################
    Sub initialise_highscores()
        Dim counter As Byte
        For counter = 0 To 9
            highScoreName(counter) = "N00B"
            highScoreValue(counter) = 0
        Next

    End Sub

    '#####################################################
    Sub Main_menu()
        Console.Clear()

        'ASCII art title
        Console.SetCursorPosition(0, 0)
        Console.WriteLine("        _            _             _                   _              _           _        ")
        Console.WriteLine("       / /\         /\ \     _    / /\                /\_\           /\ \        / /\      ")
        Console.WriteLine("      / /  \       /  \ \   /\_\ / /  \              / / /  _       /  \ \      / /  \     ")
        Console.WriteLine("     / / /\ \__   / /\ \ \_/ / // / /\ \            / / /  /\_\    / /\ \ \    / / /\ \__  ")
        Console.WriteLine("    / / /\ \___\ / / /\ \___/ // / /\ \ \          / / /__/ / /   / / /\ \_\  / / /\ \___\ ")
        Console.WriteLine("    \ \ \ \/___// / /  \/____// / /  \ \ \        / /\_____/ /   / /_/_ \/_/  \ \ \ \/___/ ")
        Console.WriteLine("     \ \ \     / / /    / / // / /___/ /\ \      / /\_______/   / /____/\      \ \ \       ")
        Console.WriteLine(" _    \ \ \   / / /    / / // / /_____/ /\ \    / / /\ \ \     / /\____\/  _    \ \ \      ")
        Console.WriteLine("/_/\__/ / /  / / /    / / // /_________/\ \ \  / / /  \ \ \   / / /______ /_/\__/ / /      ")
        Console.WriteLine("\ \/___/ /  / / /    / / // / /_       __\ \_\/ / /    \ \ \ / / /_______\\ \/___/ /       ")
        Console.WriteLine(" \_____\/   \/_/     \/_/ \_\___\     /____/_/\/_/      \_\_\\/__________/ \_____\/        ")

        'Options
        Console.SetCursorPosition(3, 13)
        Console.Write("1. Play")
        Console.SetCursorPosition(3, 14)
        Console.Write("2. Options")
        Console.SetCursorPosition(3, 15)
        Console.Write("3. Shop")
        Console.SetCursorPosition(3, 16)
        Console.Write("4. Credits")

        menuOption = 0

        Do
            get_input()
        Loop Until menuOption <> 0

        Select Case menuOption
            Case 1
                Difficulty_menu()
        End Select
    End Sub

    '#####################################################
    Sub Difficulty_menu()

        Console.Clear()
        Console.SetCursorPosition(5, 5)


        Console.Write("Ｄｉｆｆｉｃｕｌｔｙ　Ｍｅｎｕ")

        Console.SetCursorPosition(9, 8)
        Console.Write("1. Easy")
        Console.SetCursorPosition(9, 10)
        Console.Write("2. Normal")
        Console.SetCursorPosition(9, 12)
        Console.Write("3. Hard")
        Console.SetCursorPosition(9, 14)
        Console.Write("4. Insane")
        difficulty = 0
        Do
            get_input()
        Loop Until difficulty <> 0
    End Sub

    '#####################################################
    Sub display_arena()
        Dim counterx, countery As Byte
        'Console.CursorVisible = False
        Console.Clear()
        Console.SetCursorPosition(55, 2)
        Console.Write("Score: ", score)
        Console.SetCursorPosition(55, 4)
        Console.Write("Bonus: 200")

        'Draws right border
        Console.BackgroundColor = ConsoleColor.DarkRed
        For countery = 0 To 24
            Console.SetCursorPosition(50, countery)
            Console.Write(" ")
        Next

        'Draws bottom border
        For counterx = 0 To 49
            Console.SetCursorPosition(counterx, 24)
            Console.Write(" ")
        Next
        Console.BackgroundColor = 0

        'Draws all entities stored in arena array
        For counterx = 0 To 49
            For countery = 0 To 23
                Console.SetCursorPosition(counterx, countery)
                Console.Write(arena(counterx, countery))
            Next
        Next

    End Sub

    '#####################################################
    Sub get_input()
        Dim key As System.ConsoleKeyInfo

        If Console.KeyAvailable Then key = Console.ReadKey(True)

        If ((key.KeyChar = "a") And (wormDirection <> "r")) Then wormDirection = "l"
        If ((key.KeyChar = "d") And (wormDirection <> "l")) Then wormDirection = "r"
        If ((key.KeyChar = "w") And (wormDirection <> "d")) Then wormDirection = "u"
        If ((key.KeyChar = "s") And (wormDirection <> "u")) Then wormDirection = "d"

        'game pause
        If ((key.KeyChar = "p")) Then
            pause = Not (pause)
        End If

        'difficulty menu
        If difficulty = 0 Or menuOption = 0 Then
            Select Case key.KeyChar
                Case "1"
                    difficulty = 0.5
                    menuOption = 1
                Case "2"
                    difficulty = 1
                    menuOption = 2
                Case "3"
                    difficulty = 3
                    menuOption = 3
                Case "4"
                    difficulty = 5
                    menuOption = 4
            End Select

        End If

    End Sub

    Sub pause_game()
        If pause = True Then
            Dim counterX As SByte
            Console.SetCursorPosition(16, 12)
            Console.WriteLine("G A M E   P A U S E D")
            Do
                get_input()
            Loop Until pause = False
            For counterx = 16 To 37
                Console.SetCursorPosition(counterX, 12)
                Console.Write(arena(counterX, 12))
            Next
            If arena(xFood, yFood) <> "+" Then
                Console.SetCursorPosition(xFood, yFood)
                Console.Write("+")
            End If

        End If
    End Sub

    '#####################################################
    Sub collision_detection_and_score()
        If bonusLevel = False Then
            If arena(worm(wormHeadPointer, 1), worm(wormHeadPointer, 2)) = "+" Then
                wormGrow = True
                score = score + 10 + foodTimer
                Console.SetCursorPosition(55, 2)
                Console.Write("Score: {0}", score)

                'game speed reduced by difficulty level
                gameSpeed = gameSpeed - difficulty
                drop_food()
            End If
        Else
            Dim color As Byte
            If arena(worm(wormHeadPointer, 1), worm(wormHeadPointer, 2)) = "+" Then
                'lowers count of bonus fruit by one to signal end of bonus levle
                bonusFruitCount = bonusFruitCount - 1
                'code to make worm grow once every 10 fruits so you don't grow exponetially
                If bonusFruitCount Mod 100 = 0 Then
                    wormGrow = True
                End If

                'default scoring algorithm  
                score = score + 10 + foodTimer
                    Console.SetCursorPosition(55, 2)
                    Console.Write("Score: {0}", score)

                ' BONUS LEVEL EXCLUSIVE generates random integer from 1-15 to correspond with a .net console colour enum value (excluding 0 & 15 - black & white)
                color = Rnd() * 14
                    Console.BackgroundColor = color


                End If
                bonusLevelDone = True
        End If

        If arena(worm(wormHeadPointer, 1), worm(wormHeadPointer, 2)) = "#" Then
            wormAlive = False
        End If

    End Sub

    '#####################################################
    Sub move_worm()
        Dim wormHeadX As SByte
        Dim wormHeadY As SByte


        'calculate new head position
        wormHeadX = worm(wormHeadPointer, 1)
        wormHeadY = worm(wormHeadPointer, 2)
        wormHeadPointer = wormHeadPointer + 1
        If (wormHeadPointer = 100) Then wormHeadPointer = 0
        Select Case wormDirection
            Case "r"
                wormHeadX = wormHeadX + 1
            Case "l"
                wormHeadX = wormHeadX - 1
            Case "u"
                wormHeadY = wormHeadY - 1
            Case "d"
                wormHeadY = wormHeadY + 1
        End Select

        'wrap the worm around screen if needed
        If (wormHeadX = 50) Then wormHeadX = 0
        If (wormHeadX = -1) Then wormHeadX = 49
        If (wormHeadY = 24) Then wormHeadY = 0
        If (wormHeadY = -1) Then wormHeadY = 23

        'set new worm head position
        worm(wormHeadPointer, 1) = wormHeadX
        worm(wormHeadPointer, 2) = wormHeadY
        collision_detection_and_score()
        arena(wormHeadX, wormHeadY) = "#"

        'draw worm head
        Console.SetCursorPosition(worm(wormHeadPointer, 1), worm(wormHeadPointer, 2))
        Console.Write("#")

        'delete segment from tail
        If wormGrow = False Then
            Console.SetCursorPosition(worm(wormTailPointer, 1), worm(wormTailPointer, 2))
            Console.Write(" ")
            arena(worm(wormTailPointer, 1), worm(wormTailPointer, 2)) = " "
            wormTailPointer = wormTailPointer + 1
            If wormTailPointer = 100 Then
                wormTailPointer = 0
            End If
        Else
            wormGrow = False
        End If
    End Sub

    '#####################################################
    Sub game_over_message()
        Dim key As Char
        Console.SetCursorPosition(17, 12)
        Console.Write("G A M E   O V E R")
        System.Threading.Thread.Sleep(3000)
        Console.SetCursorPosition(14, 14)
        Console.Write("Press any key to continue")
        key = Console.ReadKey.KeyChar
    End Sub

    '#####################################################
    Sub high_scores()
        Dim counter1, counter2 As Int16
        Dim key As Char
        Dim isHighScore As Boolean
        Dim name As String
        'initialise local vars
        isHighScore = False
        counter1 = 0

        'Check if high score
        Do
            If (score > highScoreValue(counter1)) Then isHighScore = True
            If (isHighScore <> True) Then counter1 = counter1 + 1
        Loop Until (counter1 = 9) Or (isHighScore = True)

        'if highscore is true then ask for name and add to list
        If (isHighScore = True) Then
            Console.Clear()
            Console.SetCursorPosition(5, 5)
            Console.Write("N E W   H I G H   S C O R E")
            Console.SetCursorPosition(5, 7)
            Console.Write("Please enter your name non-noob: ")
            name = Console.ReadLine()

            'move other scores down 1 place
            For counter2 = 9 To counter1 + 1 Step -1
                highScoreName(counter2) = highScoreName(counter2 - 1)
                highScoreValue(counter2) = highScoreValue(counter2 - 1)
            Next

            'add high score to list
            highScoreName(counter1) = name
            highScoreValue(counter1) = score
        End If

        'output high score
        Console.Clear()
        Console.SetCursorPosition(5, 5)
        Console.Write("T O D A Y 'S   H I G H   S C O R E S")
        For counter1 = 0 To 9
            Console.SetCursorPosition(5, 7 + counter1)
            Console.Write(highScoreName(counter1))
            Console.SetCursorPosition(20, 7 + counter1)
            Console.Write(highScoreValue(counter1))
        Next
        Console.SetCursorPosition(5, 23)
        Console.Write("Press a key to play again")
        key = Console.ReadKey.KeyChar
    End Sub

    '#####################################################
    'change bonus timer
    Sub update_food_time()
        If (foodTimer > 0) Then
            foodTimer = foodTimer - 1
        End If
        Console.SetCursorPosition(55, 4)
        Console.Write("Bonus: {0}", foodTimer)
    End Sub
    '#####################################################
    'Super secret bonus level
    Sub bonus_level()
        Dim counter1 As Int16

        If score > bonusLevelScore And bonusLevelDone = False Then 'And gameSpeed < 49

            bonusLevel = True

            For counter1 = 1 To 10

                Console.SetCursorPosition(8, 12)
                Console.WriteLine("B O N U S   L E V E L   U N L O C K E D")
                Threading.Thread.Sleep(100)
                For counterx = 8 To 49
                    Console.SetCursorPosition(counterx, 12)
                    Console.Write(arena(counterx, 12))

                Next
                Threading.Thread.Sleep(100)
            Next
            If arena(xFood, yFood) <> "+" Then
                Console.SetCursorPosition(xFood, yFood)
                Console.Write("+")
            End If


            For counter1 = 1 To 300
                drop_food()
            Next
            bonusFruitCount = 300

        End If

        If bonusLevel = True Then
            If bonusFruitCount = 0 Then
                Console.SetCursorPosition(55, 8)
                Console.Write("Bonus level ended")
                reset_colors(0)
                bonusLevel = False
                bonusLevelDone = True
            End If
        End If
    End Sub

    '#####################################################
    Sub reset_colors(color)
        Console.BackgroundColor = color
        Console.ForegroundColor = 15

        display_arena()
        move_worm()
    End Sub
    '#####################################################
    Sub gen_obstacle()

    End Sub
    '#####################################################
    Sub Main()
        initialise_highscores()
        Do
            initialise_game()

            'Menu system
            Do
                Main_menu()
            Loop Until difficulty <> 0

            display_arena()
                Do

                    System.Threading.Thread.Sleep(gameSpeed)
                    get_input()
                    move_worm()
                    get_input()
                    pause_game()
                    bonus_level()
                    update_food_time()
                Loop Until (wormAlive = False)
                Console.BackgroundColor = 0
                game_over_message()
                high_scores()
            Loop Until (forever)

    End Sub

End Module
