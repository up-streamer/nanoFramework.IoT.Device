// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace Iot.Device.Ssd13xx.Samples
{
    // NOTE: As mentioned in GitHub issue #189 (Need OLED Graphics API).
    // https://github.com/dotnet/iot/issues/189
    // Until then, this is a basic uppercase-only font library.
    internal class BasicFont
    {
        private static IDictionary<char, byte[]> FontCharacterData =>
            new Dictionary<char, byte[]>
            {
                // Special Characters.
                { ' ', new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
                { '!', new byte[] { 0x00, 0x00, 0x5F, 0x00, 0x00, 0x00 } },
                { '\"', new byte[] { 0x00, 0x06, 0x00, 0x06, 0x00, 0x00 } },
                { '#', new byte[] { 0x14, 0x7F, 0x14, 0x7F, 0x14, 0x00 } },
                { '$', new byte[] { 0x04, 0x2A, 0x6D, 0x2A, 0x10, 0x00 } },
                { '%', new byte[] { 0x27, 0x16, 0x08, 0x34, 0x32, 0x00 } },
                { '&', new byte[] { 0x20, 0x56, 0x49, 0x36, 0x50, 0x00 } },
                { '\'', new byte[] { 0x00, 0x03, 0x05, 0x00, 0x00, 0x00 } },
                { '(', new byte[] { 0x00, 0x00, 0x1D, 0x22, 0x41, 0x00 } },
                { ')', new byte[] { 0x41, 0x22, 0x1D, 0x00, 0x00, 0x00 } },
                { '*', new byte[] { 0x14, 0x08, 0x3E, 0x08, 0x14, 0x00 } },
                { '+', new byte[] { 0x20, 0x56, 0x49, 0x36, 0x50, 0x00 } },
                { ',', new byte[] { 0x00, 0x50, 0x30, 0x00, 0x00, 0x00 } },
                { '-', new byte[] { 0x08, 0x08, 0x08, 0x08, 0x08, 0x00 } },
                { '.', new byte[] { 0x00, 0x30, 0x30, 0x00, 0x00, 0x00 } },
                { '/', new byte[] { 0x20, 0x10, 0x08, 0x04, 0x02, 0x00 } },
                { ':', new byte[] { 0x00, 0x36, 0x36, 0x00, 0x00, 0x00 } },
                { ';', new byte[] { 0x00, 0x56, 0x36, 0x00, 0x00, 0x00 } },
                { '<', new byte[] { 0x08, 0x14, 0x22, 0x41, 0x00, 0x00 } },
                { '=', new byte[] { 0x14, 0x14, 0x14, 0x14, 0x14, 0x00 } },
                { '>', new byte[] { 0x00, 0x41, 0x22, 0x14, 0x08, 0x00 } },
                { '?', new byte[] { 0x02, 0x01, 0x51, 0x09, 0x06, 0x00 } },
                { '@', new byte[] { 0x3E, 0x41, 0x4D, 0x4D, 0x06, 0x00 } },
                { '[', new byte[] { 0x00, 0x7F, 0x41, 0x41, 0x00 } },
                { '\\', new byte[] { 0x02, 0x04, 0x08, 0x10, 0x20 } },
                { ']', new byte[] { 0x00, 0x41, 0x41, 0x7F, 0x00 } },
                { '^', new byte[] { 0x04, 0x02, 0x01, 0x02, 0x04 } },
                { '`', new byte[] { 0x00, 0x00, 0x05, 0x03, 0x00 } },

                // Numbers.
                { '0', new byte[] { 0x3E, 0x51, 0x49, 0x45, 0x3E, 0x00 } },
                { '1', new byte[] { 0x00, 0x42, 0x7F, 0x40, 0x00, 0x00 } },
                { '2', new byte[] { 0x42, 0x61, 0x51, 0x49, 0x46, 0x00 } },
                { '3', new byte[] { 0x21, 0x41, 0x45, 0x4B, 0x31, 0x00 } },
                { '4', new byte[] { 0x18, 0x14, 0x12, 0x7F, 0x10, 0x00 } },
                { '5', new byte[] { 0x27, 0x45, 0x45, 0x45, 0x39, 0x00 } },
                { '6', new byte[] { 0x3C, 0x4A, 0x49, 0x49, 0x30, 0x00 } },
                { '7', new byte[] { 0x01, 0x71, 0x09, 0x05, 0x03, 0x00 } },
                { '8', new byte[] { 0x36, 0x49, 0x49, 0x49, 0x36, 0x00 } },
                { '9', new byte[] { 0x06, 0x49, 0x49, 0x29, 0x1E, 0x00 } },

                // Characters.
                { 'A', new byte[] { 0x7E, 0x11, 0x11, 0x11, 0x7E, 0x00 } },
                { 'B', new byte[] { 0x7F, 0x49, 0x49, 0x49, 0x36, 0x00 } },
                { 'C', new byte[] { 0x3E, 0x41, 0x41, 0x41, 0x22, 0x00 } },
                { 'D', new byte[] { 0x7F, 0x41, 0x41, 0x22, 0x1C, 0x00 } },
                { 'E', new byte[] { 0x7F, 0x49, 0x49, 0x49, 0x41, 0x00 } },
                { 'F', new byte[] { 0x7F, 0x09, 0x09, 0x09, 0x01, 0x00 } },
                { 'G', new byte[] { 0x3E, 0x41, 0x49, 0x49, 0x7A, 0x00 } },
                { 'H', new byte[] { 0x7F, 0x08, 0x08, 0x08, 0x7F, 0x00 } },
                { 'I', new byte[] { 0x00, 0x41, 0x7F, 0x41, 0x00, 0x00 } },
                { 'J', new byte[] { 0x20, 0x40, 0x41, 0x3F, 0x01, 0x00 } },
                { 'K', new byte[] { 0x7F, 0x08, 0x14, 0x22, 0x41, 0x00 } },
                { 'L', new byte[] { 0x7F, 0x40, 0x40, 0x40, 0x40, 0x00 } },
                { 'M', new byte[] { 0x7F, 0x02, 0x0C, 0x02, 0x7F, 0x00 } },
                { 'N', new byte[] { 0x7F, 0x04, 0x08, 0x10, 0x7F, 0x00 } },
                { 'O', new byte[] { 0x3E, 0x41, 0x41, 0x41, 0x3E, 0x00 } },
                { 'P', new byte[] { 0x7F, 0x09, 0x09, 0x09, 0x06, 0x00 } },
                { 'Q', new byte[] { 0x3E, 0x41, 0x51, 0x21, 0x5E, 0x00 } },
                { 'R', new byte[] { 0x7F, 0x09, 0x19, 0x29, 0x46, 0x00 } },
                { 'S', new byte[] { 0x46, 0x49, 0x49, 0x49, 0x31, 0x00 } },
                { 'T', new byte[] { 0x01, 0x01, 0x7F, 0x01, 0x01, 0x00 } },
                { 'U', new byte[] { 0x3F, 0x40, 0x40, 0x40, 0x3F, 0x00 } },
                { 'V', new byte[] { 0x1F, 0x20, 0x40, 0x20, 0x1F, 0x00 } },
                { 'W', new byte[] { 0x3F, 0x40, 0x38, 0x40, 0x3F, 0x00 } },
                { 'X', new byte[] { 0x63, 0x14, 0x08, 0x14, 0x63, 0x00 } },
                { 'Y', new byte[] { 0x07, 0x08, 0x70, 0x08, 0x07, 0x00 } },
                { 'Z', new byte[] { 0x61, 0x51, 0x49, 0x45, 0x43, 0x00 } },
                // Small letters
                { 'a', new byte[] { 0x20, 0x54, 0x54, 0x54, 0x78 } },
                { 'b', new byte[] { 0x7F, 0x48, 0x44, 0x44, 0x38 } },
                { 'c', new byte[] { 0x38, 0x44, 0x44, 0x44, 0x20 } },
                { 'd', new byte[] { 0x38, 0x44, 0x44, 0x48, 0x7F } },
                { 'e', new byte[] { 0x38, 0x54, 0x54, 0x54, 0x18 } },
                { 'f', new byte[] { 0x08, 0x7E, 0x09, 0x01, 0x02 } },
                { 'g', new byte[] { 0x04, 0x2A, 0x2A, 0x2A, 0x1C } },
                { 'h', new byte[] { 0x7F, 0x08, 0x04, 0x04, 0x78 } },
                { 'i', new byte[] { 0x00, 0x44, 0x7D, 0x40, 0x00 } },
                { 'j', new byte[] { 0x20, 0x40, 0x44, 0x3D, 0x00 } },
                { 'k', new byte[] { 0x7F, 0x10, 0x28, 0x44, 0x00 } },
                { 'l', new byte[] { 0x00, 0x41, 0x7F, 0x40, 0x00 } },
                { 'm', new byte[] { 0x7C, 0x04, 0x18, 0x04, 0x78 } },
                { 'n', new byte[] { 0x7C, 0x08, 0x04, 0x04, 0x78 } },
                { 'o', new byte[] { 0x38, 0x44, 0x44, 0x44, 0x38 } },
                { 'p', new byte[] { 0x7C, 0x14, 0x14, 0x14, 0x08 } },
                { 'q', new byte[] { 0x08, 0x14, 0x14, 0x18, 0x7C } },
                { 'r', new byte[] { 0x7C, 0x08, 0x04, 0x04, 0x08 } },
                { 's', new byte[] { 0x48, 0x54, 0x54, 0x54, 0x20 } },
                { 't', new byte[] { 0x04, 0x3F, 0x44, 0x40, 0x20 } },
                { 'u', new byte[] { 0x3C, 0x40, 0x40, 0x20, 0x7C } },
                { 'v', new byte[] { 0x1C, 0x20, 0x40, 0x20, 0x1C } },
                { 'w', new byte[] { 0x3C, 0x40, 0x30, 0x40, 0x3C } },
                { 'x', new byte[] { 0x44, 0x28, 0x10, 0x28, 0x44 } },
                { 'y', new byte[] { 0x0C, 0x50, 0x50, 0x50, 0x3C } },
                { 'z', new byte[] { 0x44, 0x64, 0x54, 0x4C, 0x44 } }
            };

        public static byte[] GetCharacterBytes(char character)
        {
            return FontCharacterData[character];
        }
    }
}
