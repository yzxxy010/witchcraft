using System;

using System.IO;

using System.Collections.Generic;

using System.Runtime.InteropServices;

using FMOD;

using FMODUnity;

using NeoModLoader.services;

using UnityEngine;

namespace VideoCopilot.code

{

    public class PlayWavDirectly

    {

        private static PlayWavDirectly _instance;

        public static PlayWavDirectly Instance => _instance ??= new PlayWavDirectly();

        private FMOD.System fmodSystem;

        private ChannelGroup masterChannelGroup;

        public FMOD.VECTOR fmodPosition;

        public FMOD.VECTOR zeroVel;

        private PlayWavDirectly()

        {

            // Initialize FMOD system

            InitializeFMODSystem();

        }

        private void InitializeFMODSystem()

        {

            // Use RuntimeManager to get the StudioSystem, then retrieve the CoreSystem

            var result = RuntimeManager.StudioSystem.getCoreSystem(out fmodSystem);

            if (result != FMOD.RESULT.OK)

            {

                LogService.LogError($"Failed to initialize FMOD Core System. Result: {result}");

                return;

            }

            // Get the master channel group

            result = fmodSystem.getMasterChannelGroup(out masterChannelGroup);

            if (result != FMOD.RESULT.OK)

            {

                LogService.LogError($"Failed to retrieve master channel group. Result: {result}");

            }

        }

        ///


        /// Play a .wav file directly using FMOD.

        ///


        /// The path to the .wav file.

        public void PlaySoundFromFile(string filePath)

        {

            if (!File.Exists(filePath))

            {

                LogService.LogError($"File not found at path: {filePath}");

                return;

            }

            try

            {

                // Create a sound from the file

                FMOD.Sound sound;

                RESULT result = fmodSystem.createSound(filePath, MODE.DEFAULT, out sound);

               

                if (result != RESULT.OK)

                {

                    LogService.LogError($"FMOD failed to create sound: {result}");

                    return;

                }

                // Play the sound

                Channel channel;

                result = fmodSystem.playSound(sound, masterChannelGroup, false, out channel);

                if (result != RESULT.OK)

                {

                    LogService.LogError($"FMOD failed to play sound: {result}");

                    sound.release();

                    return;

                }

                LogService.LogInfo($"Playing sound: {filePath}");

            }

            catch (Exception ex)

            {

                LogService.LogError($"Error while playing sound: {ex.Message}");

            }

        }

        public void PlaySoundAtPosition(string filePath, Vector3 position)

        {

            if (!File.Exists(filePath))

            {

                LogService.LogError($"File not found at path: {filePath}");

                return;

            }

            try

            {

                // Create a sound from the file

                FMOD.Sound sound;

                RESULT result = fmodSystem.createSound(filePath, MODE.DEFAULT, out sound);

               

                if (result != RESULT.OK)

                {

                    LogService.LogError($"FMOD failed to create sound: {result}");

                    return;

                }

                // Play the sound

                Channel channel;

                result = fmodSystem.playSound(sound, masterChannelGroup, false, out channel);

                if (result != RESULT.OK)

                {

                    LogService.LogError($"FMOD failed to play sound: {result}");

                    sound.release();

                    return;

                }

                //3d Stuff

                // Set the 3D attributes (position) of the sound

                Vector3 sourcePosition = new Vector3

                {

                    x = position.x,

                    y = position.y,

                    z = position.z

                };

                Vector3 listenerPosition = Camera.main.transform.position;

                zeroVel = new FMOD.VECTOR{x=0f,y=0f,z=0f};

                channel.setVolume(0.1f);

               

                float normalizedDistance = Mathf.Clamp01(Vector3.Distance(sourcePosition, listenerPosition) / 300.0f);

                channel.setVolume(0.1f * (1f - normalizedDistance));

                LogService.LogInfo("Played sound using position: " + sourcePosition.x + " / " + sourcePosition.y + " / " + sourcePosition.z +" with the main camera at: " + listenerPosition + " -- NORMALIZED Distance at: " + normalizedDistance);

            }

            catch (Exception ex)

            {

                LogService.LogError($"Error while playing sound: {ex.Message}");

            }

        }

    }

}