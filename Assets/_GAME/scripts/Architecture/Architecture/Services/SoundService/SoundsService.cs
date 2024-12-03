using System;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _GAME.scripts.Architecture.Architecture.Services.SoundService
{
	public class SoundsService
	{
		private SoundsFactory _soundsFactory;
		private Dictionary<SoundID, AudioSource> activeSounds = new Dictionary<SoundID, AudioSource>();
		private bool _isMuted = false;

		[Inject]
		private void Construct(SoundsFactory soundsFactory)
		{
			_soundsFactory = soundsFactory;
		}

		public void SetMuteForAll(bool isMute)
		{
			// if(_isMuted == isMute)
			// 	return;
			//
			// _isMuted = isMute;
			if (isMute)
				PauseAll();
			else
				PlayAll();
		}

		public void Play(SoundID id, bool playAgain = false)
		{
			if (playAgain && IsSoundPlaing(id))
			{
				Stop(id);
				InitSound(id);
			}
			if (!IsSoundPlaing(id))
			{
				InitSound(id);
			}
		}

		public void Stop(SoundID id)
		{
			if (IsSoundPlaing(id))
			{
				AudioSource audioSource;
				activeSounds.TryGetValue(id, out audioSource);
				Object.Destroy(audioSource!.gameObject);
			}
		}

		private bool IsSoundPlaing(SoundID id)
		{
			AudioSource audioSource;
			activeSounds.TryGetValue(id, out audioSource);
			return audioSource != null;
		}

		private AudioSource InitSound(SoundID id)
		{
			AudioSource activeUI = _soundsFactory.CreateSound(id);
			if (activeUI == null)
				throw new Exception($"[QWINO ERRROR] Could found SOUND with {id}. It doesn't found");
			SetSoundAsActive(id, activeUI);
			return activeUI;
		}

		private void SetSoundAsActive(SoundID id, AudioSource activeUI)
		{
			if (IsSoundPlaing(id))
			{
				activeSounds[id].Stop();
			}
			activeSounds[id] = activeUI;
		}

		public void PlayAll()
		{
			AudioListener.pause = false;
		}

		public void PauseAll()
		{
			AudioListener.pause = true;
		}
	}
}
