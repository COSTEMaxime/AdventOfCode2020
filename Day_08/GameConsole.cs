using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_08
{
    class GameConsole
    {
        private int Pc;
        public int Acc { get; private set; }
        public int Status { get; private set; }
        private Instruction[] Instructions;
        private Instruction[] BackupInstructions;

        private int InstructionSwapCounter;

        public GameConsole(string[] rawOpcodes)
        {
            Pc = 0;
            Acc = 0;
            Status = 0;
            InstructionSwapCounter = 0;

            LoadProgram(rawOpcodes);
        }

        private void LoadProgram(string[] rawOpcodes)
        {
            Instructions = new Instruction[rawOpcodes.Length];
            BackupInstructions = new Instruction[rawOpcodes.Length];

            for (int i = 0; i < rawOpcodes.Length; i++)
            {
                string[] tokens = rawOpcodes[i].Split(' ');
                Instructions[i] = new Instruction(tokens[0], int.Parse(tokens[1]));
                BackupInstructions[i] = new Instruction(tokens[0], int.Parse(tokens[1]));
            }
        }

        public void Reset()
        {
            for (int i = 0; i < BackupInstructions.Length; i++)
            {
                Instructions[i] = BackupInstructions[i];
            }

            Pc = 0;
            Acc = 0;
            Status = 0;
        }

        public void Run()
        {
            Instruction nextInstruction = Instructions[Pc];
            while (nextInstruction.AlreadyExecuted == false)
            {
                Instructions[Pc].AlreadyExecuted = true;
                switch (nextInstruction.Opcode)
                {
                    case "acc":
                        Acc += nextInstruction.Argument;
                        Pc++;
                        break;
                    case "jmp":
                        Pc += nextInstruction.Argument;
                        break;
                    case "nop":
                        Pc++;
                        break;
                }

                if (Pc >= Instructions.Length) {
                    Status = -1;
                    return;
                }
                else if (Pc < 0)
                {
                    return;
                }

                nextInstruction = Instructions[Pc];
            }
        }

        public void SwapNextInstruction()
        {
            if (Instructions[InstructionSwapCounter].Opcode == "nop")
            {
                Instructions[InstructionSwapCounter].Opcode = "jmp";
                InstructionSwapCounter++;
            }
            else if (Instructions[InstructionSwapCounter].Opcode == "jmp")
            {
                Instructions[InstructionSwapCounter].Opcode = "nop";
                InstructionSwapCounter++;
            }
            else
            {
                InstructionSwapCounter++;
                SwapNextInstruction();
            }
        }
    }

    public struct Instruction
    {
        public string Opcode { get; set; }
        public int Argument { get; }
        public bool AlreadyExecuted { get; set; }

        public Instruction(string opcode, int argument)
        {
            this.Opcode = opcode;
            this.Argument = argument;
            AlreadyExecuted = false;
        }
    }
}
