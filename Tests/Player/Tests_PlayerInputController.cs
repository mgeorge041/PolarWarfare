using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PlayerNS;

namespace Tests.PlayerNS_Tests
{
    public class Tests_PlayerInputController : MonoBehaviour
    {
        private PlayerInputController inputController;


        // Setup
        [SetUp]
        public void Setup()
        {
            inputController = PlayerInputController.InstantiatePlayerInputController();
        }


        // Test creates input controller
        [Test]
        public void CreatesPlayerInputController()
        {
            Assert.IsNotNull(inputController);
        }
    }
}