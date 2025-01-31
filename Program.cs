using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
//snac_game_cont_type = analogizer_config_s[4:0];
//snac_cont_assignment = analogizer_config_s[9:6];
//analogizer_video_type = analogizer_config_s[13:10];
//analogizer_ena = analogizer_config_s[5];
//pocket_blank_screen = analogizer_config_s[14];
//analogizer_osd_out = analogizer_config_s[15];
class Program
{
    static string NL = Environment.NewLine; // shortcut
    static string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
    static string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
    static string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
    static string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
    static string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
    static string MAGENTA = Console.IsOutputRedirected ? "" : "\x1b[95m";
    static string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
    static string GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
    static string BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
    static string NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
    static string UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
    static string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";
    static string REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
    static string NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

    static int videoSelection = 0;
    static int snacAssigmentSelection = 0;
    static int snacSelection = 0;
    static readonly int analogizerEnaSelection = 1;
    static int pocketBlankScreenSelection = 0;
    static int analogizerOsdOutSelection = 1;
    static string AnalogizerHeader = @"
===============================================================================
   ###   #     #   ###   #         ####   #######    #### ####### ####### #### 
  #...#  ##    #. #...#  #.       #....#  #.          #..  ....#. #...... #...#
 #.    # #.#   #.#.    # #.      #.     #.#.          #.      #.  #.      #.  #
 #.    #.#. #  #.#.    #.#.      #.     #.#.  ####    #.     #.   ####### ####.
 #######.#.  # #.#######.#.      #.     #.#.   ...#   #.    #.    #...... #.#. 
 #.....#.#.   ##.#.....#.#.      #.     #.#.      #.  #.   #.     #.      #. #.
 #.    #.#.    #.#.    #.#.       #.   #. #.      #.  #.  #.      #.      #.  #
 #.    #.#.    #.#.    #.#######   ####.   #######.  #### ####### ####### #.  #
                                                                               
                       C O N F I G U R A T O R  V0.1 BETA                      
===============================================================================
";
    static readonly Dictionary<int, string> MenuOptions = new Dictionary<int, string>
    {
        {1, "SNAC game controller options"},
        {2, "Video output options"},
        {3, "Miscellaneous options"},
        {4, "Save and Exit"}
    };

    static readonly Dictionary<int, string> VideoOutputOptions = new Dictionary<int, string>
    {
        {0, "RGBS"},
        {1, "RGsB"},
        {2, "YPbPr"},
        {3, "Y/C NTSC"},
        {4, "Y/C PAL"},
        {5, "SC 0% RGBHV"},
        {6, "SC 25% RGBHV"},
        {7, "SC 50% RGBHV"},
        {8, "SC 75% RGBHV"},
        {9, "SC HQ2x RGBHV"}
    };

    //static Dictionary<int, string> AnalogizerEnableOptions = new Dictionary<int, string>
    //{
    //    {1, "On"},
    //    {0, "Off"}
    //};

    static readonly Dictionary<int, string> SNACassigmentsOptions = new Dictionary<int, string>
    {
        {0, "SNAC P1 -> Pocket P1"},        //0x00
        {1, "SNAC P1 -> Pocket P2"},        //0x40
        {2, "SNAC P1,P2 -> Pocket P1,P2"},  //0x80
        {3, "SNAC P1,P2 -> Pocket P2,P1"},  //0xC0
        {4, "SNAC P1,P2 -> Pocket P3,P4"},  //0x100
        {5, "SNAC P1-P4 -> Pocket P1-P4"},  //0x200
    };

    static readonly Dictionary<int, string> SNACSelectionOptions = new Dictionary<int, string>
    {
        {0, "None - No SNAC gamepad, use Pocket and/or Dock controls"},
        {1, "DB15 Normal - Neogeo/Arcade using DB15 connector (normal polling speed)"},
        {2, "NES - Nintendo Entertainment System gamepad"},
        {3, "SNES - Super Nintendo gamepad"},
        {4, "PCE 2btn - PC Engine gamepad with 2 buttons"},
        {5, "PCE 6btn - PC Engine gamepad with 6 buttons"},
        {6, "PCE Multitap - Multitap for PC Engine"},
        {0x9, "DB15 Fast - Neogeo/Arcade using DB15 connector (fast polling speed)"},
        {0xb, "SNES A,B<->X,Y - SNES with remapped buttons"},
        {0x11, "PSX (Digital PAD) - PlayStation 1/2 digital gamepad"},
        {0x13, "PSX (Analog PAD) - PlayStation 1/2 analog gamepad"}
    };


    static readonly Dictionary<int, string> PocketBlankScreenOptions = new Dictionary<int, string>
    {
        {1, "Pocket Blank Screen ON"},
        {0, "Pocket Blank Screen OFF"}
    };

    static readonly Dictionary<int, string> AnalogizerOSDOptions = new Dictionary<int, string>
    {
        {1, "OSD is show on Analogizer video output (when avalaible)"},
        {0, "OSD is show on Pocket screen (when avalaible)"}
    };
    static void FlushKeyboard()
    {
        while (Console.In.Peek() != -1)
            Console.In.Read();
    }

    private static void SnacOptions()
    {
        while (snacSelection == -1)
        {
            Console.Clear();

            //Opciones de selección de mando SNAC
            // Console.WriteLine($"\n\n{GREY}{REVERSE}=== SNAC Game Controller Selection:==={NOREVERSE}{NORMAL}");
            Console.WriteLine($"=== SNAC Game Controller Selection:===");
            foreach (var option in SNACSelectionOptions)
            {
                Console.WriteLine("{0}: {1}", option.Key, option.Value);
                //Console.WriteLine("{0}{1}: {2}{3}", GREEN, option.Key, NORMAL, option.Value);
            }
            Console.WriteLine("");
            Console.Write($"Select an option:");
            if (int.TryParse(Console.ReadLine(), out int input) && SNACSelectionOptions.ContainsKey(input))
            {
                snacSelection = input;
            }
            else
            {
                FlushKeyboard();
                Console.WriteLine($"Option not valid.Try again.");
                Console.ReadLine(); // Espera a que el usuario presione Enter
                snacSelection = -1; // Reinicia la selección de video para repetir el menú completo
            }
        }
        
        while (snacAssigmentSelection == -1)
        {
            //Opciones asignación de mando SNAC
            Console.WriteLine($"\n\n=== SNAC Game Controller Assignments Selection:===");
            foreach (var option in SNACassigmentsOptions)
            {
                Console.WriteLine("{0}: {1}", option.Key, option.Value);
            }
            Console.WriteLine("");
            Console.Write($"Select an option:");
            if (int.TryParse(Console.ReadLine(), out int input) && SNACassigmentsOptions.ContainsKey(input))
            {
                snacAssigmentSelection = input;
            }
            else
            {
                FlushKeyboard();
                Console.WriteLine($"Option not valid.Try again.");
                Console.ReadLine(); // Espera a que el usuario presione Enter
                snacAssigmentSelection = -1; // Reinicia la selección de video para repetir el menú completo
            }
        }
    }

    private static void VideoOptions()
    {
        while (videoSelection == -1)
        {
            Console.Clear();
            //Opciones de selección de salida de video
            Console.WriteLine($"=== Video Output Options ===");
            foreach (var option in VideoOutputOptions)
            {
                Console.WriteLine("{0}: {1}", option.Key, option.Value);
            }
            Console.WriteLine("");
            Console.Write($"Select an option:");
            if (int.TryParse(Console.ReadLine(), out int input) && VideoOutputOptions.ContainsKey(input))
            {
                videoSelection = input;
            }
            else
            {
                Console.WriteLine($"Option not valid.Try again.");
                Console.ReadLine(); // Espera a que el usuario presione Enter
                videoSelection = -1; // Reinicia la selección de video para repetir el menú completo
            }
        }
       
        while (pocketBlankScreenSelection == -1)
        {
                //Opciones de pocket Blank Screen
                Console.WriteLine($"\n\n=== Pocket Blank Screen Options: ===");
            foreach (var option in PocketBlankScreenOptions)
            {
                Console.WriteLine("{0}: {1}", option.Key, option.Value);
            }
            Console.WriteLine("");
            Console.Write($"Select an option:");
            if (int.TryParse(Console.ReadLine(), out int input) && PocketBlankScreenOptions.ContainsKey(input))
            {
                pocketBlankScreenSelection = input;
            }
            else
            {
                Console.WriteLine($"Option not valid.Try again.");
                Console.ReadLine(); // Espera a que el usuario presione Enter
                pocketBlankScreenSelection = -1; // Reinicia la selección de video para repetir el menú completo
            }
        }
    }
    private static void MiscOptions()
    {
        while (analogizerOsdOutSelection == -1)
        {
            Console.Clear();
            //Opciones de selección de salida de video
            Console.WriteLine($"=== Analogizer OSD Options: ===");
            foreach (var option in AnalogizerOSDOptions)
            {
                Console.WriteLine("{0}: {1}", option.Key, option.Value);
            }
            Console.WriteLine("");
            Console.Write($"Select an option:");
            if (int.TryParse(Console.ReadLine(), out int input) && AnalogizerOSDOptions.ContainsKey(input))
            {
                analogizerOsdOutSelection = input;
            }
            else
            {
                Console.WriteLine($"Option not valid.Try again.");
                Console.ReadLine(); // Espera a que el usuario presione Enter
                analogizerOsdOutSelection = -1; // Reinicia la selección de video para repetir el menú completo
            }
        }
    }
    static void Main(string[] args)
    {
        int menuDone = -1;

        while (menuDone != 4)
        {
            Console.Clear();
            // Console.WriteLine($"{CYAN}{REVERSE}{AnalogizerHeader}");
            // Console.WriteLine($"{REVERSE}======================= C U R R E N T   S E T T I N G S ======================={NOREVERSE}");
            // string msg = string.Format("{0,-40}", SNACSelectionOptions[snacSelection]);
            // Console.WriteLine("{0}SNAC Controller:    {1}{2,-40}", GREY, YELLOW, SNACSelectionOptions[snacSelection]);
            // Console.WriteLine("{0}SNAC Assigments:    {1}{2,-40}", GREY, YELLOW, SNACassigmentsOptions[snacAssigmentSelection]);
            // Console.WriteLine("{0}SNAC Video output:  {1}{2,-40}", GREY, YELLOW, VideoOutputOptions[videoSelection]);
            // Console.WriteLine("{0}Pocket Blank Screen:{1}{2,-40}", GREY, YELLOW, PocketBlankScreenOptions[pocketBlankScreenSelection]);
            // Console.WriteLine("{0}OSD output:         {1}{2,-40}", GREY, YELLOW, AnalogizerOSDOptions[analogizerOsdOutSelection]);
            // Console.WriteLine($"{NORMAL}{REVERSE}==============================================================================={NOREVERSE}");
            Console.WriteLine($"{AnalogizerHeader}");
            Console.WriteLine($"======================= C U R R E N T   S E T T I N G S =======================");
            string msg = string.Format("{0,-40}", SNACSelectionOptions[snacSelection]);
            Console.WriteLine("SNAC Controller:    {0,-40}", SNACSelectionOptions[snacSelection]);
            Console.WriteLine("SNAC Assigments:    {0,-40}", SNACassigmentsOptions[snacAssigmentSelection]);
            Console.WriteLine("SNAC Video output:  {0,-40}", VideoOutputOptions[videoSelection]);
            Console.WriteLine("Pocket Blank Screen:{0,-40}", PocketBlankScreenOptions[pocketBlankScreenSelection]);
            Console.WriteLine("OSD output:         {0,-40}", AnalogizerOSDOptions[analogizerOsdOutSelection]);
            Console.WriteLine($"{NORMAL}{REVERSE}==============================================================================={NOREVERSE}");

            Console.WriteLine("");
            foreach (var option in MenuOptions)
            {
                Console.WriteLine("{0}: {1}", option.Key, option.Value);
            }
            Console.WriteLine("");
            Console.Write($"Select an option:");
            if (int.TryParse(Console.ReadLine(), out int input) && VideoOutputOptions.ContainsKey(input))
            {
                menuDone = input;

                switch (menuDone)
                {
                    case 1:
                        {
                            //SNAC game controller options
                            snacSelection = -1;
                            snacAssigmentSelection = -1;
                            SnacOptions();
                            break;
                        }

                    case 2:
                        {
                            //Video output options
                            videoSelection = -1;
                            pocketBlankScreenSelection = -1;
                            VideoOptions();
                            break;
                        }
                    case 3:
                        {
                            //Miscellaneous options
                            analogizerOsdOutSelection = -1;
                            MiscOptions();
                            break;
                        }
                    case 4:
                        {
                            //Exit
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                FlushKeyboard();
                Console.WriteLine("Option not valid.Try again.");
                Console.ReadLine(); // Espera a que el usuario presione Enter
                menuDone = -1; // Reinicia la selección de video para repetir el menú completo
            }

        }


        //snac_game_cont_type = analogizer_config_s[4:0];
        //snac_cont_assignment = analogizer_config_s[9:6];
        //analogizer_video_type = analogizer_config_s[13:10];
        ////analogizer_ena		  = analogizer_config_s[5];	
        ////pocket_blank_screen   = analogizer_config_s[14];
        ////analogizer_osd_out	  = analogizer_config_s[15];

        // Almacenar la selección en un archivo binario de 32 bits con big-endian
        uint data = (uint)((analogizerOsdOutSelection << 15) | (pocketBlankScreenSelection << 14) | (videoSelection << 10) | (snacAssigmentSelection << 6) | (analogizerEnaSelection << 5) | snacSelection); // Usamos uint para 32 bits
        byte[] buffer = BitConverter.GetBytes(data);
        //Array.Reverse(buffer); // Invertimos el arreglo para big-endian

        File.WriteAllBytes("analogizer.bin", buffer);

        Console.WriteLine("Analogizer configuration saved to 'analogizer.bin'.");
        Console.WriteLine("Press a key to close...");
        Console.ReadKey(); ; // Espera a que el usuario presione Enter antes de cerrar el programa*/
    }
}