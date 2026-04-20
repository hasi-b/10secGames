# EMG-Controlled Mini-Games (Obrigado – Extended)

An experimental extension of a narrative mini-game collection, exploring EMG-based biosignals as an alternative input method.

---

## Overview

This project is a collection of short, 10-second mini-games originally created to reflect personal experiences of arriving in a new country. Each mini-game captures a small moment of adaptation, uncertainty, or daily life.

The extended version introduces EMG (electromyography) signals as the primary input, replacing conventional controls with physiological data. Interaction is driven by muscle activity, creating a different relationship between player and system.

---

## Research Focus

The project explores how interaction changes when input is derived from the body rather than explicit actions.

Key questions include:

- How can EMG signals be used as a reliable control mechanism?
- Can a new control mechanism can be integrated into a primarily one button input game

---

## Interaction Design

- Muscle activation triggers actions within mini-games
- Input is continuous but noisy, requiring filtering and thresholding
- Feedback is designed to remain readable despite signal variability
- Control is indirect, requiring adaptation from the player

The system shifts interaction from deliberate button presses to subtle physical signals.

---

## Signal Processing (OpenBCI + EMG Joystick Demo)

The EMG input was implemented using **OpenBCI hardware** together with an existing open-source repository (**EMG-Joystick-Demo**) as the base signal processing pipeline.

- EMG signals were captured using OpenBCI sensors
- The EMG-Joystick-Demo repository was used for initial signal acquisition and basic gesture mapping
- The system provided real-time EMG values representing muscle activation intensity
- These values were streamed into Unity for gameplay interaction
- Unity handled the mapping between incoming EMG values and in-game actions (continuous and discrete control)

---

## Design Approach

The project follows a design-led research process:

- Start from an existing interaction system
- Replace the input layer with a physiological signal
- Iterate on feedback and control mapping
- Evaluate how interaction changes under new constraints

---

## Outcome

The project demonstrates that:

- EMG signals can function as a viable, though imperfect, control method
- Interaction becomes less precise but more embodied
- Feedback design is critical when working with unstable input systems

---

## Future Directions

- Multi-channel EMG for richer input
- Adaptive calibration systems per user
- Combining EMG with other modalities (e.g., motion or audio)
- Applications in accessibility and assistive interaction

---

## Roles

- Interaction Design  
- Gameplay Programming  
- Technical Development  

---
