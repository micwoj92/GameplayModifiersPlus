﻿namespace GamePlayModifiersPlus.Multiplayer
{
    using AsyncTwitch;
    using System.Collections;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    using GamePlayModifiersPlus;
    public class MultiPowers : MonoBehaviour
    {
        public static IEnumerator ChargeOverTime()
        {
            yield return new WaitForSeconds(MultiMain.Config.timeForCharges);
            Plugin.charges += MultiMain.Config.chargesOverTime;
            if (Plugin.charges > MultiMain.Config.maxCharges) Plugin.charges = MultiMain.Config.maxCharges;
            Plugin.twitchPowers.StartCoroutine(ChargeOverTime());
        }


        public static IEnumerator TempDA(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " DA | ";
            Plugin.spawnController.SetField("_disappearingArrows", true);
            yield return new WaitForSeconds(length);
            Plugin.spawnController.SetField("_disappearingArrows", false);
            text.text = text.text.Replace(" DA | ", "");
        }

        public static IEnumerator CoolDown(float waitTime, string cooldown, string message)
        {

            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().cooldownText;
            text.text += " " + cooldown + " | ";
            Plugin.cooldowns.SetCooldown(true, cooldown);
            if (MultiMain.Config.showCooldownOnMessage)
            {
                if (MultiMain.Config.globalCommandCooldown > 0 && Plugin.cooldowns.GetCooldown("Global") == false)
                {
                    TwitchConnection.Instance.SendChatMessage(message + " " + cooldown + " Cooldown Active for " + waitTime.ToString() + " seconds." + "Global Command Cooldown Active for " + MultiMain.Config.globalCommandCooldown + " seconds.");
                }
                else
                    TwitchConnection.Instance.SendChatMessage(message + " " + cooldown + " Cooldown Active for " + waitTime.ToString() + " seconds");
            }
            else
                TwitchConnection.Instance.SendChatMessage(message);


            yield return new WaitForSeconds(waitTime);
            Plugin.cooldowns.SetCooldown(false, cooldown);
            text.text = text.text.Replace(" " + cooldown + " | ", "");
        }

        public static IEnumerator GlobalCoolDown()
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().cooldownText;

            Plugin.cooldowns.SetCooldown(true, "Global");
            yield return new WaitForSeconds(MultiMain.Config.globalCommandCooldown);
            Plugin.cooldowns.SetCooldown(false, "Global");
            text.text = text.text.Replace(" " + "Global" + " | ", "");
        }

        public static IEnumerator TempInstaFail(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " InstaFail | ";
            Image energyBar = Plugin.energyPanel.GetField<Image>("_energyBar");
            energyBar.color = Color.red;
            Plugin.energyCounter.SetField("_badNoteEnergyDrain", 1f);
            Plugin.energyCounter.SetField("_missNoteEnergyDrain", 1f);
            Plugin.energyCounter.SetField("_hitBombEnergyDrain", 1f);
            Plugin.energyCounter.SetField("_obstacleEnergyDrainPerSecond", 1f);
            yield return new WaitForSeconds(length);
            energyBar.color = Color.white;
            Plugin.energyCounter.SetField("_badNoteEnergyDrain", 0.1f);
            Plugin.energyCounter.SetField("_missNoteEnergyDrain", 0.15f);
            Plugin.energyCounter.SetField("_hitBombEnergyDrain", 0.15f);
            Plugin.energyCounter.SetField("_obstacleEnergyDrainPerSecond", 0.1f);
            text.text = text.text.Replace(" InstaFail | ", "");
        }

        public static IEnumerator TempInvincibility(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " Invincible | ";
            Image energyBar = Plugin.energyPanel.GetField<Image>("_energyBar");
            energyBar.color = Color.yellow;
            Plugin.energyCounter.SetField("_badNoteEnergyDrain", 0f);
            Plugin.energyCounter.SetField("_missNoteEnergyDrain", 0f);
            Plugin.energyCounter.SetField("_hitBombEnergyDrain", 0f);
            Plugin.energyCounter.SetField("_obstacleEnergyDrainPerSecond", 0f);
            yield return new WaitForSeconds(length);
            energyBar.color = Color.white;
            Plugin.energyCounter.SetField("_badNoteEnergyDrain", 0.1f);
            Plugin.energyCounter.SetField("_missNoteEnergyDrain", 0.15f);
            Plugin.energyCounter.SetField("_hitBombEnergyDrain", 0.15f);
            Plugin.energyCounter.SetField("_obstacleEnergyDrainPerSecond", 0.1f);
            text.text = text.text.Replace(" Invincible | ", "");
        }

        public static IEnumerator SpecialEvent()
        {
            Plugin.gnomeActive = true;
            yield return new WaitForSecondsRealtime(0.1f);
            Plugin.SetTimeScale(0f);
            Time.timeScale = 0f;
            Plugin.gnomeSound.Load();
            Plugin.gnomeSound.Play();
            Plugin.soundIsPlaying = true;
            Plugin.Log("Waiting");
            yield return new WaitForSecondsRealtime(16f);
            if (Plugin.isValidScene == true)
            {
                Plugin.soundIsPlaying = false;
                Plugin.SetTimeScale(1f); ;
                Time.timeScale = 1f;
                Plugin.songAudio.pitch = 1f;
                Plugin.Log("Unpaused");
                Plugin.gnomeActive = false;
            }
        }

        public static void AdjustNJS(float njs)
        {
            /*
            Log("_halfJumpDurationInBeats " + spawnController.GetField<float>("_halfJumpDurationInBeats").ToString());
            Log("_spawnAheadTime " + spawnController.GetField<float>("_spawnAheadTime").ToString());
            Log("_jumpDistance " + spawnController.GetField<float>("_jumpDistance").ToString());
            Log("_noteJumpMovementSpeed " + spawnController.GetField<float>("_noteJumpMovementSpeed").ToString());
            Log("_moveDistance " + spawnController.GetField<float>("_moveDistance").ToString());
            Log("_moveSpeed " + spawnController.GetField<float>("_moveSpeed").ToString());
            Log("_moveDurationInBeats " + spawnController.GetField<float>("_moveDurationInBeats").ToString());
            Log("_beatsPerMinut e" + spawnController.GetField<float>("_beatsPerMinute").ToString());
            Log("_moveDurationInBeats " + spawnController.GetField<float>("_moveDurationInBeats").ToString());
            */

            float halfJumpDur = 1f;
            float maxHalfJump = 18f;
            float minHalfJump = 8f;
            float moveSpeed = Plugin.spawnController.GetField<float>("_moveSpeed");
            float moveDir = Plugin.spawnController.GetField<float>("_moveDurationInBeats");
            float jumpDis;
            float spawnAheadTime;
            float moveDis;
            float bpm = Plugin.spawnController.GetField<float>("_beatsPerMinute");
            float num = 60f / bpm;
            moveDis = moveSpeed * num * moveDir;
            /*
                while (njs * num * halfJumpDur > maxHalfJump)
            {
                    halfJumpDur -= 1f;
            }

                while (njs * num * halfJumpDur < minHalfJump)
            {
                    halfJumpDur += 1f;
            }
         */
            halfJumpDur = Plugin.spawnController.GetField<float>("_halfJumpDurationInBeats");
            jumpDis = njs * num * halfJumpDur * 2f;
            spawnAheadTime = moveDis / moveSpeed + jumpDis * 0.5f / njs;
            Plugin.spawnController.SetField("_halfJumpDurationInBeats", halfJumpDur);
            Plugin.spawnController.SetField("_spawnAheadTime", spawnAheadTime);
            Plugin.spawnController.SetField("_jumpDistance", jumpDis);
            Plugin.spawnController.SetField("_noteJumpMovementSpeed", njs);
            Plugin.spawnController.SetField("_moveDistance", moveDis);
        }

        public static IEnumerator Wait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
        }

        public static IEnumerator ScaleNotes(float scale, float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " Smaller/Larger | ";
            Plugin.altereddNoteScale = scale;
            yield return new WaitForSeconds(length);
            Plugin.altereddNoteScale = 1f;
            text.text = text.text.Replace(" Smaller/Larger | ", "");
        }

        public static IEnumerator RandomNotes(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " Random | ";
            GMPUI.randomSize = true;
            yield return new WaitForSeconds(length);
            GMPUI.randomSize = false;
            text.text = text.text.Replace(" Random | ", "");
        }

        public static IEnumerator njsRandom(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " NJSRandom | ";
            GMPUI.njsRandom = true;
            yield return new WaitForSeconds(length);
            GMPUI.njsRandom = false;
            AdjustNJS(Plugin.songNJS);
            text.text = text.text.Replace(" NJSRandom | ", "");
        }

        public static IEnumerator Pause()
        {
            yield return new WaitForSeconds(0f);
            StandardLevelGameplayManager pauseManager = Resources.FindObjectsOfTypeAll<StandardLevelGameplayManager>().First();
            pauseManager.HandlePauseTriggered();
        }

        public static IEnumerator TempNoArrows(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " NoArrows | ";
            yield return new WaitForSeconds(0f);
            GameplayCoreSceneSetup gameplayCoreSceneSetup = Resources.FindObjectsOfTypeAll<GameplayCoreSceneSetup>().First();
            BeatmapDataModel dataModel = gameplayCoreSceneSetup.GetField<BeatmapDataModel>("_beatmapDataModel");
            Plugin.Log("Grabbed dataModel");
            BeatmapData beatmapData = dataModel.beatmapData;
            BeatmapObjectData[] objects;
            NoteData note;
            float start = Plugin.songAudio.time + 2;
            float end = start + length + 2f;
            foreach (BeatmapLineData line in beatmapData.beatmapLinesData)
            {
                objects = line.beatmapObjectsData;
                foreach (BeatmapObjectData beatmapObject in objects)
                {
                    if (beatmapObject.beatmapObjectType == BeatmapObjectType.Note)
                        if (beatmapObject.time > start && beatmapObject.time < end)
                        {
                            note = beatmapObject as NoteData;

                            note.SetNoteToAnyCutDirection();
                            note.TransformNoteAOrBToRandomType();

                        }

                }
            }
            yield return new WaitForSeconds(length + 2f);
            text.text = text.text.Replace(" NoArrows | ", "");
            //    dataModel.beatmapData = beatmapData;
        }

        public static IEnumerator RandomBombs(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " Bombs | ";


            yield return new WaitForSeconds(0f);
            GameplayCoreSceneSetup gameplayCoreSceneSetup = Resources.FindObjectsOfTypeAll<GameplayCoreSceneSetup>().First();
            BeatmapDataModel dataModel = gameplayCoreSceneSetup.GetField<BeatmapDataModel>("_beatmapDataModel");
            Plugin.Log("Grabbed dataModel");
            BeatmapData beatmapData = dataModel.beatmapData;
            BeatmapObjectData[] objects;
            NoteData note;
            float start = Plugin.songAudio.time + 2;
            float end = start + length + 2f;
            foreach (BeatmapLineData line in beatmapData.beatmapLinesData)
            {
                objects = line.beatmapObjectsData;
                foreach (BeatmapObjectData beatmapObject in objects)
                {
                    if (beatmapObject.beatmapObjectType == BeatmapObjectType.Note)
                        if (beatmapObject.time > start && beatmapObject.time < end)
                        {
                            try
                            {
                                //                        Plugin.Log("Attempting to Convert to Bomb");
                                note = beatmapObject as NoteData;

                                int randMax = (int)((1 / MultiMain.Config.bombChance) * 100);
                                int randMin = 100;
                                int random = Random.Range(1, randMax);

                                //                Plugin.Log("Min: " + randMin + " Max: " + randMax + " Number: " + random);

                                if (random <= randMin || MultiMain.Config.bombChance == 1)
                                    note.SetProperty("noteType", NoteType.Bomb);
                            }
                            catch (System.Exception ex)
                            {
                                Plugin.Log(ex.ToString());
                            }


                        }

                }
            }
            yield return new WaitForSeconds(length + 2f);

            text.text = text.text.Replace(" Bombs | ", "");
            dataModel.beatmapData = beatmapData;
        }

        public static IEnumerator SpeedChange(float length, float pitch)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " Speed | ";
            GameplayCoreSceneSetup sceneSetup = Resources.FindObjectsOfTypeAll<GameplayCoreSceneSetup>().First();
      //      if (Plugin.pauseManager.gameState != StandardLevelGameplayManager.GameState.Playing) yield break;
            Plugin.AudioTimeSync.SetField("_timeScale", pitch);
            Plugin.songAudio.pitch = pitch;
      //      Plugin.AudioTimeSync.Init(Plugin.levelData.difficultyBeatmap.level.audioClip, Plugin.songAudio.time, Plugin.levelData.difficultyBeatmap.level.songTimeOffset, pitch);
            AudioMixerSO mixer = sceneSetup.GetField<AudioMixerSO>("_audioMixer");
            mixer.musicPitch = 1f / pitch;
      //      Plugin.AudioTimeSync.forcedAudioSync = true;

            float songspeedmul = Plugin.levelData.gameplayCoreSetupData.gameplayModifiers.songSpeedMul;

            yield return new WaitForSeconds(length);
        //    if (Plugin.pauseManager.gameState != StandardLevelGameplayManager.GameState.Playing) yield break;
            Plugin.AudioTimeSync.SetField("_timeScale", songspeedmul);
            Plugin.songAudio.pitch = songspeedmul;
            //        Plugin.AudioTimeSync.Init(Plugin.levelData.difficultyBeatmap.level.audioClip, Plugin.songAudio.time, Plugin.levelData.difficultyBeatmap.level.songTimeOffset, songspeedmul);
            mixer.musicPitch = 1 / songspeedmul;
            if (songspeedmul == 1f) mixer.musicPitch = 1;
  //          if(songspeedmul == 1f) Plugin.AudioTimeSync.forcedAudioSync = false;


            //     Plugin.AudioTimeSync.SetField("didInit", false);
            //      Plugin.AudioTimeSync.Init(Plugin.levelData.difficultyBeatmap.level.audioClip, Plugin.songAudio.time, Plugin.levelData.difficultyBeatmap.level.songTimeOffset, pitch);
            text.text = text.text.Replace(" Speed | ", "");
        }



            public static IEnumerator NoArrows()
        {
            Plugin.Log("Starting");

            yield return new WaitForSeconds(0f);
            GameplayCoreSceneSetup gameplayCoreSceneSetup = Resources.FindObjectsOfTypeAll<GameplayCoreSceneSetup>().First();
            BeatmapDataModel dataModel = gameplayCoreSceneSetup.GetField<BeatmapDataModel>("_beatmapDataModel");
            Plugin.Log(dataModel.beatmapData.bombsCount.ToString());
            BeatmapData beatmapData = dataModel.beatmapData;
            BeatmapObjectData[] objects;
            NoteData note;
            foreach (BeatmapLineData line in beatmapData.beatmapLinesData)
            {
                objects = line.beatmapObjectsData;
                foreach (BeatmapObjectData beatmapObject in objects)
                {
                    if (beatmapObject.beatmapObjectType == BeatmapObjectType.Note)
                    {
                        note = beatmapObject as NoteData;

                        note.SetNoteToAnyCutDirection();
                    }




                }
            }
            //    dataModel.beatmapData = beatmapData;
        }


        public static IEnumerator Funky(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " Funky | ";
            GMPUI.funky = true;
            yield return new WaitForSeconds(length);
            GMPUI.funky = false;
            text.text = text.text.Replace(" Funky | ", "");
        }

        public static IEnumerator Rainbow(float length)
        {
            var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
            text.text += " Rainbow | ";
            GMPUI.rainbow = true;
            yield return new WaitForSeconds(length);
            GMPUI.rainbow = false;
            Plugin.colorA.SetColor(Plugin.oldColorA);
            Plugin.colorB.SetColor(Plugin.oldColorB);
            text.text = text.text.Replace(" Rainbow | ", "");
        }

        public static void ResetPowers(bool resetMessage)
        {
            GMPUI.rainbow = false;
            GMPUI.funky = false;
            GMPUI.njsRandom = false;
            GMPUI.randomSize = false;
            Plugin.altereddNoteScale = 1f;
            Time.timeScale = 1;
            Plugin.timeScale = 1;
            Plugin.superRandom = false;
            if (resetMessage)
            {
                Plugin.spawnController.SetField("_disappearingArrows", false);
                Plugin.colorA.SetColor(Plugin.oldColorA);
                Plugin.colorB.SetColor(Plugin.oldColorB);
                var text = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().cooldownText;
                text.text = " ";
                var text2 = GameObject.Find("Chat Powers").GetComponent<GamePlayModifiersPlus.TwitchStuff.GMPDisplay>().activeCommandText;
                if (text2.text.Contains("NoArrows"))
                {
                    text2.text = "";
                    text2.text += " NoArrows | ";
                    if (text2.text.Contains("Bombs"))
                    text2.text += " Bombs | ";

                }
                else if (text2.text.Contains("Bombs"))
                {
                    text2.text = "";
                    text2.text += " Bombs | ";
                }
                else
                    text2.text = "";
                GameplayCoreSceneSetup sceneSetup = Resources.FindObjectsOfTypeAll<GameplayCoreSceneSetup>().First();
                AudioMixerSO mixer = sceneSetup.GetField<AudioMixerSO>("_audioMixer");
                float songspeedmul = Plugin.levelData.gameplayCoreSetupData.gameplayModifiers.songSpeedMul;
                Plugin.AudioTimeSync.SetField("_timeScale", songspeedmul);
                Plugin.songAudio.pitch = songspeedmul;
                mixer.musicPitch = 1 / songspeedmul;
                if (songspeedmul == 1f) mixer.musicPitch = 1;

                resetMessage = false;
            }

        }
    }
}
