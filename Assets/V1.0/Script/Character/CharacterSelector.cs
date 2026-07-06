#region Summary
//<Summary/>
// This script manages character activation based on the current hour. It supports both real device time and a compressed time cycle for testing.
// Each character is assigned a time range (startHour to endHour) during which it will
// be active. The script logs detailed information about the current mode, hour, and character activation status for debugging purposes.
//  Key Features:
// 1. Time Modes: Switch between real device time and a compressed time cycle.
// 2. Character Slots: Define multiple character slots with specific active time ranges.
// 3. Debug Logging: Provides detailed logs for current time, mode, and character activation
//    status to assist with debugging and verification.
// Usage:
// 1. Attach this script to a GameObject in your Unity scene.
// 2. Configure the character slots in the Inspector, assigning GameObjects and their active time
//    ranges.
// 3. Choose whether to use compressed time for testing or real device time.
// 4. Run the scene to see characters activate based on the current hour, with logs providing insights into the process.
// Note: Ensure that the assigned GameObjects in the character slots are properly set up in the scene for the activation to work as intended.
// This script is designed to be flexible and can be easily extended or modified to fit specific game requirements, such as adding more complex time-based behaviors or integrating with other game systems.
// Author: Towhidul Islam Rahat
// Date: 2026-04-31
// Version: 1.0
//</Summary/>
#endregion
#region Change Log
//<ChangeLog/>
// Version 1.0 - Initial implementation of CharacterSelector script with time-based character activation and
// detailed debug logging.
#endregion
#region Milestone 3 Sprint 1 - Character Selection System Test
using System;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [Serializable]
    public class CharacterSlot
    {
        public GameObject character;
        public int startHour;
        public int endHour;
    }

    [Header("Time Settings")]
    [SerializeField] bool useCompressedTime = true;
    [SerializeField] float cycleDuration = 30f;

    [Header("Characters")]
    [SerializeField] CharacterSlot[] slots;

    void Awake()
    {
        int hour = useCompressedTime ? GetCompressedHour() : GetRealHour();

        Debug.Log($"[CharacterSelector] Mode: {(useCompressedTime ? "Compressed" : "Real")} | Current hour: {hour}");

        ActivateCharacter(hour);
    }

    void Update()
    {
        if (!useCompressedTime) return;

        int hour = GetCompressedHour();
        ActivateCharacter(hour);
    }

    int GetRealHour()
    {
        int hour = DateTime.Now.Hour;
        Debug.Log($"[CharacterSelector] Real device time - Hour: {hour}");
        return hour;
    }

    int GetCompressedHour()
    {
        float progress = (Time.time % cycleDuration) / cycleDuration;
        int hour = Mathf.FloorToInt(progress * 24);
        return hour;
    }

    void ActivateCharacter(int hour)
    {
        bool anyActivated = false;

        for (int i = 0; i < slots.Length; i++)
        {
            CharacterSlot slot = slots[i];

            if (slot.character == null)
            {
                Debug.LogWarning($"[CharacterSelector] Slot {i} has no character assigned. Skipping.");
                continue;
            }

            bool inRange = hour >= slot.startHour && hour <= slot.endHour;
            slot.character.SetActive(inRange);

            if (inRange)
            {
                Debug.Log($"[CharacterSelector] Activated: {slot.character.name} | Slot: {slot.startHour}:00 - {slot.endHour}:59 | Hour: {hour}");
                anyActivated = true;
            }
        }

        if (!anyActivated)
        {
            Debug.LogWarning($"[CharacterSelector] No character matched hour {hour}. All characters are inactive.");
        }
    }
}
#endregion
#region Milestone 3 Sprint 2 - Character Selection System Test with Real Time
//using System;
//using UnityEngine;

//public class CharacterSelector : MonoBehaviour
//{
//    [Serializable]
//    public class CharacterSlot
//    {
//        public GameObject character;
//        public int startHour;
//        public int endHour;
//    }

//    [Header("Time Settings")]
//    [SerializeField] bool useCompressedTime = false;
//    [SerializeField] float cycleDuration = 30f;

//    [Header("Characters")]
//    [SerializeField] CharacterSlot[] slots;

//    void Awake()
//    {
//        int hour = useCompressedTime ? GetCompressedHour() : GetRealHour();

//        Debug.Log($"[CharacterSelector] Mode: {(useCompressedTime ? "Compressed" : "Real")} | Current hour: {hour}");

//        ActivateCharacter(hour);
//    }

//    void Update()
//    {
//        if (!useCompressedTime) return;

//        int hour = GetCompressedHour();
//        ActivateCharacter(hour);
//    }

//    int GetRealHour()
//    {
//        int hour = DateTime.Now.Hour;
//        Debug.Log($"[CharacterSelector] Real device time - Hour: {hour}");
//        return hour;
//    }

//    int GetCompressedHour()
//    {
//        float progress = (Time.time % cycleDuration) / cycleDuration;
//        int hour = Mathf.FloorToInt(progress * 24);
//        return hour;
//    }

//    void ActivateCharacter(int hour)
//    {
//        bool anyActivated = false;

//        for (int i = 0; i < slots.Length; i++)
//        {
//            CharacterSlot slot = slots[i];

//            if (slot.character == null)
//            {
//                Debug.LogWarning($"[CharacterSelector] Slot {i} has no character assigned. Skipping.");
//                continue;
//            }

//            bool inRange = hour >= slot.startHour && hour <= slot.endHour;
//            slot.character.SetActive(inRange);

//            if (inRange)
//            {
//                Debug.Log($"[CharacterSelector] Activated: {slot.character.name} | Slot: {slot.startHour}:00 - {slot.endHour}:59 | Hour: {hour}");
//                anyActivated = true;
//            }
//        }

//        if (!anyActivated)
//        {
//            Debug.LogWarning($"[CharacterSelector] No character matched hour {hour}. All characters are inactive.");
//        }
//    }
//}
#endregion
//#region Milestone 3 Sprint 3 - Character Selection System Test with Sunday Restriction
//using System; 
//using UnityEngine;

//public class CharacterSelector : MonoBehaviour
//{
//    [Serializable]
//    public class CharacterSlot
//    {
//        public GameObject character;
//        public int startHour;
//        public int endHour;
//        public bool disabledOnSunday;
//    }

//    [Header("Debug Settings")]
//    [Header("Time Settings")]
//    [SerializeField] bool useCompressedTime = true;
//    [SerializeField] float cycleDuration = 30f;
//    [SerializeField] bool simulateSunday = false;

//    [Header("Characters")]
//    [SerializeField] CharacterSlot[] slots;

//    void Awake()
//    {
//        // In compressed time mode, Time.time is 0 in Awake, which always resolves to
//        // hour 0 — incorrectly deactivating all characters that don't cover that slot.
//        // Let Update handle activation from the first frame instead.
//        if (useCompressedTime) return;

//        int hour = GetRealHour();
//        bool isSunday = simulateSunday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;

//        Debug.Log($"[CharacterSelector] Mode: Real | Hour: {hour} | Sunday: {isSunday}");

//        ActivateCharacter(hour, isSunday);
//    }

//    void Update()
//    {
//        if (!useCompressedTime) return;

//        int hour = GetCompressedHour();
//        bool isSunday = simulateSunday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;

//        ActivateCharacter(hour, isSunday);
//    }

//    int GetRealHour()
//    {
//        int hour = DateTime.Now.Hour;
//        Debug.Log($"[CharacterSelector] Real device time - Hour: {hour}");
//        return hour;
//    }

//    int GetCompressedHour()
//    {
//        float progress = (Time.time % cycleDuration) / cycleDuration;
//        int hour = Mathf.FloorToInt(progress * 24);
//        return hour;
//    }

//    void ActivateCharacter(int hour, bool isSunday)
//    {
//        bool anyActivated = false;

//        for (int i = 0; i < slots.Length; i++)
//        {
//            CharacterSlot slot = slots[i];

//            if (slot.character == null)
//            {
//                Debug.LogWarning($"[CharacterSelector] Slot {i} has no character assigned. Skipping.");
//                continue;
//            }

//            bool inRange = hour >= slot.startHour && hour <= slot.endHour;
//            bool blockedBySunday = isSunday && slot.disabledOnSunday;
//            bool shouldActivate = inRange && !blockedBySunday;

//            if (slot.character.activeSelf != shouldActivate)
//                slot.character.SetActive(shouldActivate);

//            if (shouldActivate)
//            {
//                Debug.Log($"[CharacterSelector] Activated: {slot.character.name} | Slot: {slot.startHour}:00 - {slot.endHour}:59 | Hour: {hour}");
//                anyActivated = true;
//            }
//        }

//        if (!anyActivated)
//            Debug.LogWarning($"[CharacterSelector] No character matched hour {hour} (Sunday: {isSunday}). All characters inactive.");
//    }
//}
//#endregion

//using System;
//using UnityEngine;

//public class CharacterSelector : MonoBehaviour
//{
//    [Serializable]
//    public class CharacterSlot
//    {
//        public GameObject character;
//        public int startHour;
//        public int endHour;
//        public bool disabledOnSunday;
//    }

//    [Header("Debug Settings")]
//    [Header("Time Settings")]
//    [SerializeField] bool useCompressedTime = true;
//    [SerializeField] float cycleDuration = 30f;
//    [SerializeField] bool simulateSunday = false;

//    [Header("Characters")]
//    [SerializeField] CharacterSlot[] slots;

//    void Awake()
//    {
//        if (useCompressedTime) return;

//        int hour = GetRealHour();
//        bool isSunday = simulateSunday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;

//        Debug.Log($"[CharacterSelector] Mode: Real | Hour: {hour} | Sunday: {isSunday}");

//        ActivateCharacter(hour, isSunday);
//    }

//    void Update()
//    {
//        if (!useCompressedTime) return;

//        int hour = GetCompressedHour();
//        bool isSunday = simulateSunday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;

//        ActivateCharacter(hour, isSunday);
//    }

//    int GetRealHour()
//    {
//        int hour = DateTime.Now.Hour;
//        Debug.Log($"[CharacterSelector] Real device time - Hour: {hour}");
//        return hour;
//    }

//    int GetCompressedHour()
//    {
//        float progress = (Time.time % cycleDuration) / cycleDuration;
//        int hour = Mathf.FloorToInt(progress * 24);
//        return hour;
//    }

//    void ActivateCharacter(int hour, bool isSunday)
//    {
//        bool anyActivated = false;

//        for (int i = 0; i < slots.Length; i++)
//        {
//            CharacterSlot slot = slots[i];

//            if (slot.character == null)
//            {
//                Debug.LogWarning($"[CharacterSelector] Slot {i} has no character assigned. Skipping.");
//                continue;
//            }

//            bool inRange = hour >= slot.startHour && hour <= slot.endHour;
//            bool blockedBySunday = isSunday && slot.disabledOnSunday;
//            bool shouldActivate = inRange && !blockedBySunday;

//            if (slot.character.activeSelf != shouldActivate)
//                slot.character.SetActive(shouldActivate);

//            if (shouldActivate)
//            {
//                Debug.Log($"[CharacterSelector] Activated: {slot.character.name} | Slot: {slot.startHour}:00 - {slot.endHour}:59 | Hour: {hour}");
//                anyActivated = true;
//            }
//        }

//        if (!anyActivated)
//            Debug.LogWarning($"[CharacterSelector] No character matched hour {hour} (Sunday: {isSunday}). All characters inactive.");
//    }
//}