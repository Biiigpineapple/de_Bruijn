using System;


/*
*   Function: de Bruijn encode and decode class
*   
*/
public class deBrujin
{
    public deBrujin()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    /*
     *  Function: generating the de Bruijn sequence with LFSR principle
     *  
        Input Arguments:
     *      1. n: the index of de Bruijn sequence
     *      
     *  Output Arguments:
     *      1. byte[]: the de Bruijn sequence.
    */

    public byte[] deBrujin_lfsr_generator(int n)
    {

        byte[] shift_register = new byte[n]; // shift register buffer

        int deBruijn_length = (int)Math.Pow(2, n) - 1; // the length of the de Bruijn sequence

        byte[] deBruijn_sequence = new byte[deBruijn_length]; // de Bruijn sequence buffer

        List<byte> coefficient_array; // the primitive polynomial of the de Bruijn sequence

        /* ===============================  generating  ================================= */

        shift_register[0] = 1; // the intial value of the register can't be all zeros.

        coefficient_array = primitive_polynomial(n); // loading the coefficient array of the primitive polynomial of de Bruijn sequence

        // If the array length less than 1, it means the 'n' value has not been supported yet.
        if (coefficient_array.Count < 1) 
        {
            Console.WriteLine("Not support this 'n' value");
            return deBruijn_sequence;
        }

        byte[] cal_bits = new byte[coefficient_array.Count]; // creat the bits which is used for new push-in LSB byte calculation

        // Based on the LFSR principle to generate the de Bruijn sequence
        for (int i = 0; i < deBruijn_length; i++)
        {
            deBruijn_sequence[i] = shift_register[0]; // push the LSB into the sequence buffer

            // Update the bits for calculation
            for (int j = 0; j < coefficient_array.Count; j++)
                cal_bits[j] = shift_register[coefficient_array[j]];

            // shift the register coefficient array
            for (int j = n - 1; j > 0; j--)
                shift_register[j] = shift_register[j - 1];

            // multi XOR calculation
            shift_register[0] = multi_byte_XOR(cal_bits);
        }

        return deBruijn_sequence;

    }

    public byte byte_XOR(byte a, byte b)
    {
        byte c = 0;

        if (a == b)
            c = 0;
        else
            c = 1;

        return c;
    }

    public byte multi_byte_XOR(byte[] bits)
    {
        byte c = bits[0];

        int length = bits.Length;

        for (int i = 1; i < length; i++)
        {
            c = byte_XOR(c, bits[i]);
        }

        return c;
    }


    /*
     *  Function: the look up table of the primitive polynomial coefficient of the de Bruijn sequence, now support n = 4, 6, 10, 16, 20, 24, 30
     *  
        Input Arguments:
     *      1. n: the index of de Bruijn sequence
     *      
     *  Output Arguments:
     *      1. List<byte>: the coefficient of the de Bruijn sequence.
    */

    public List<byte> primitive_polynomial(int n)
    {
        List<byte> coefficient_array = new List<byte> { };

        switch (n)
        {
            case 4:
                coefficient_array.Add(3);
                coefficient_array.Add(0);
                break;
            case 6:
                coefficient_array.Add(5);
                coefficient_array.Add(0);
                break;
            case 10:
                coefficient_array.Add(9);
                coefficient_array.Add(2);
                break;
            case 16:
                coefficient_array.Add(15);
                coefficient_array.Add(4);
                coefficient_array.Add(2);
                coefficient_array.Add(1);
                break;
            case 20:
                coefficient_array.Add(19);
                coefficient_array.Add(2);
                break;
            case 24:
                coefficient_array.Add(23);
                coefficient_array.Add(3);
                coefficient_array.Add(2);
                coefficient_array.Add(0);
                break;
            case 30:
                coefficient_array.Add(19);
                coefficient_array.Add(5);
                coefficient_array.Add(3);
                coefficient_array.Add(0);
                break;
        }

        return coefficient_array;
    }

}